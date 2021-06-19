using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelEF.Model;
using PagedList;

namespace ModelEF.DAO
{
    public class UserAccountDAO
    {
        NguyenVuAnhKhoaContext db = null;

        public UserAccountDAO()
        {
            db = new NguyenVuAnhKhoaContext();
        }
        //1. Phương thức kiểm tra đăng nhập
        public int Login(string userName, string passWord)
        {
            var result = db.UserAccounts.SingleOrDefault(x => x.Username == userName);
            //Nếu không tìm thấy tức tài khoản này không tồn tại, trả về 0
            if (result == null)
            {
                return 0;
            }
            else
            {
                //Nếu trạng thái tài khoản bằng Blocked thì trả về -1
                if (result.Status == "Blocked")
                {
                    return -1;
                }
                else
                {
                    //Nếu điền đúng tài khoản và mật khẩu sẽ trả về 1
                    if (result.Password == passWord)
                        return 1;
                    //Nếu điền sai mật khẩu báo mật khẩu sai sẽ trả về -2
                    else
                        return -2;
                }

            }
        }
        //2. Trả về danh sách tất cả các user
        public IEnumerable<UserAccount> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserAccount> model = db.UserAccounts;
            if (!string.IsNullOrEmpty(searchString))
                model = model.Where(x => x.Username.Contains(searchString) || x.Status.Contains(searchString));
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        //2. Phương thức thêm mới tài khoản
        public long Insert(UserAccount entity)
        {
            db.UserAccounts.Add(entity);
            db.SaveChanges();
            //Tự tăng
            return entity.ID;
        }
        //3. Phương thức cập nhật tài khoản
        public bool Update(UserAccount entity)
        {
            try
            {
                var user = db.UserAccounts.Find(entity.ID);
                user.Username = entity.Username;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                  
                
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
     
        //4. Phương thức lấy ra username
        public UserAccount GetById(string userName)
        {
            return db.UserAccounts.SingleOrDefault(x => x.Username == userName);
        }
        //5. Phương thức hiện lấy Id để cập nhật thông tin user
        public UserAccount ViewDetail(int id)
        {
            return db.UserAccounts.Find(id);
        }

       
        //7. Phương thức xóa người dùng
        public bool Delete(int id)
        {
            try
            {
                var user = db.UserAccounts.Find(id);

                db.UserAccounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


                //    //  var user = db.UserAccounts.Find(id);
                //    //  if (user.Status == "Blocked")
                //    //  {
                //    //      return true;
                //    //  }
                //    //else
                //    //   {
                //    //  return false;
                //    //  }



                //}

                //public int Delete(int id)
                //{
                //    //kiểm tra giá trị trong bảng với gtri truyền vào
                //    var user = db.UserAccounts.Find(id);
                //    //nếu có giá trị đó

                //    if (user.Status == "Blocked")
                //        {
                //            return 1;
                //        }
                //        else
                //        {
                //            return 0;
                //        }

                //    }
                //}


            }
}
