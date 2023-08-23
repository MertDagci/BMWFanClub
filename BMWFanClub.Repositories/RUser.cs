using BMWFanClub.Data;
using BMWFanClub.Data.MySQL;
using BMWFanClub.Enums;
using BMWFanClub.InAndOutModel;
using BMWFanClub.Interfaces;
using BMWFanClub.Repositories.Helper;
using System.Globalization;

namespace BMWFanClub.Repositories
{
    public class RUser : IUser
    {
        private readonly DataContext _context;
        public RUser(DataContext context)
        {
            _context = context;
        }
        public async Task<MethodResponse<MUser.Response>> Add(MUser.Form form, string currentUserId)
        {
            try
            {
                MethodResponse<MUser.Response> mr = new MethodResponse<MUser.Response>();

                //var currentUser = _context.Users.Where(k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                //if (currentUser == null)
                //{
                //    mr.Status = 400;
                //    mr.StatusTexts.Add("Kullanıcı bulunamadı!");
                //    return await Task.FromResult(mr);
                //}


                if (form.Name.Length < 2)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("İsim minimum 3 karakter olmalı");
                }
                if (form.Surname.Length < 2)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Soyad minimum 3 karakter olmalı");
                }
                if (form.Password.Length < 5)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Şifre minimum 6 karakter olmalı");
                }
                if (mr.Status != 200)
                {
                    return await Task.FromResult(mr);
                }

                #region sql
                var user = new User();
                user.Id = Guid.NewGuid().ToString();
                user.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(form.Name);
                user.Surname = form.Surname;
                user.Email = form.Email;
                user.Password = form.Password.ConvertToMD5();
                user.ValidEmail = false;
                user.CreatedBy = currentUserId;
                user.CreatedOn = DateTime.Now;

                _context.Users.Add(user);
                _context.SaveChanges();
                #endregion

                var item = new MUser.Response
                {
                    Email = user.Email,
                    Password = user.Password,
                    Name = user.Name,
                    Surname = user.Surname,
                };
                mr.Status = 200;
                mr.StatusTexts.Add("Kayıt Başarıyla Tamamlandı");
                mr.Item = item;

                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {
                throw await Task.FromResult(ex);
            }
        }

        public async Task<MethodResponse<string>> Delete(string id, string currentUserId)
        {

            try
            {
                MethodResponse<string> mr = new MethodResponse<string>();
                var currentUser = _context.Users.Where(k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (currentUser == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı bulunamadı!");
                    return await Task.FromResult(mr);
                }
                var user = _context.Users.Where(k => k.Id == id && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                //var usera = _context.Users.FirstOrDefault(k => k.Id == id && k.IsDeleted == false);
                if (user == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kişi Bulunamadı");
                    return await Task.FromResult(mr);
                }

                user.IsDeleted = EuIsDeleted.Yes;
                user.DeletedBy = currentUserId;
                user.DeletedOn = DateTime.Now;

                _context.SaveChanges();
                mr.StatusTexts.Add("Kullanıcı Başarıyla Silindi");
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }

        public Task<MethodResponse<string>> Delete(MUser.FilterForm form, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<MethodResponse<List<MUser.Response>>> MultipleGet(MUser.FilterForm form, string currentUserId)
        {
            try
            {
                MethodResponse<List<MUser.Response>> mr = new MethodResponse<List<MUser.Response>>();
                var currentUser = _context.Users.Where(k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (currentUser == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı bulunamadı!");
                    return await Task.FromResult(mr);
                }
                //ef
                //var Users = _context.Users.Where(k => k.IsDeleted == EuIsDeleted.No).Select(l => new MUser.Response
                //{
                //    Id = l.Id,
                //    Email = l.Email,
                //    Name = l.Name,
                //    NickName = l.Nickname,
                //}).ToList();

                //lınq
                var users = (from c in _context.Users
                             where (form.Search == null ? true : c.Name.Contains(form.Search.ToLower())) && c.IsDeleted == EuIsDeleted.No
                             select new MUser.Response
                             {
                                 Id = c.Id,
                                 Email = c.Email,
                                 Name = c.Name,
                                 Surname = c.Surname,
                             }).ToList();

                mr.Count = users.Count();
                mr.Item = users;
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }

        public async Task<MethodResponse<MUser.Response>> SingleGet(string id, string currentUserId)
        {
            try
            {
                MethodResponse<MUser.Response> mr = new MethodResponse<MUser.Response>();
                var currentUser = _context.Users.Where(k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (currentUser == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı bulunamadı!");
                    return await Task.FromResult(mr);
                }
                var user = _context.Users.Where(u => u.Id == id && u.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (user == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kişi Bulunamadı");
                    return await Task.FromResult(mr);
                }
                MUser.Response response = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                };
                mr.Item = response;
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }

        public async Task<MethodResponse<MUser.Response>> Update(MUser.Form form, string currentUserId)
        {
            try
            {
                MethodResponse<MUser.Response> mr = new();
                var currentUser = _context.Users.Where(k => k.Id == currentUserId && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (currentUser == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı bulunamadı!");
                    return await Task.FromResult(mr);
                }
                var user = _context.Users.Where(k => k.Id == form.Id && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (user == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı Bulunamadı");
                    return await Task.FromResult(mr);
                }

                if (form.Name.Length < 2)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("İsim minimum 3 karakter olmalı");
                }
                if (!string.IsNullOrEmpty(form.Password))
                {
                    if (form.Password.Length < 5)
                    {
                        mr.Status = 400;
                        mr.StatusTexts.Add("Şifre minimum 6 karakter olmalı");
                    }
                }

                if (mr.Status != 200)
                {
                    return await Task.FromResult(mr);
                }

                user.Name = form.Name;
                user.Email = form.Email;
                user.Surname = form.Surname;
                user.UpdatedBy = currentUserId;
                user.UpdatedOn = DateTime.Now;

                _context.SaveChanges();
                mr.StatusTexts.Add("Kişi Başarıyla Güncellendi");
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }

        public async Task<MethodResponse<MUser.Response>> Login(MUser.LoginForm form, string currentUserId)
        {
            try
            {
                MethodResponse<MUser.Response> mr = new MethodResponse<MUser.Response>();

                if (form.Email == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Email Boş Olamaz");
                }
                if (form.Password == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Şifre Boş Olamaz");
                }

                if (mr.Status != 200)
                {
                    return await Task.FromResult(mr);
                }

                var user = _context.Users.Where(k => k.Email == form.Email && k.Password == form.Password.ConvertToMD5() && k.IsDeleted == EuIsDeleted.No).FirstOrDefault();
                if (user == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Email veya şifrenizi kontrol edin!");
                    return await Task.FromResult(mr);

                }
                MUser.Response response = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email
                };
                mr.Item = response;
                mr.StatusTexts.Add("Giriş Başarılı");
                return await Task.FromResult(mr);


            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}