using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelEF.DAO;
using ModelEF.Model;
using TestUngDung.Common;
using PagedList;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserAccountController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserAccountDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        //2. Yêu cầu Tạo người dùng mới
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserAccountDAO();
                if (dao.GetById(user.Username) != null)
                {
                    SetAlert("Người dùng này đã tồn tại", "warning");
                    return RedirectToAction("Create", "UserAccount");
                }
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                long id = dao.Insert(user);
                //Nếu insert thành công tức id >0
                if (id > 0)
                {
                    SetAlert("Thêm người dùng thành công", "success");
                    return RedirectToAction("Index", "UserAccount");
                }
            }
            return View("Index");
        }
        //3. Yêu cầu Cập nhật thông tin người dùng
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserAccountDAO().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserAccountDAO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                }

                var result = dao.Update(user);

                if (result)
                {
                    SetAlert("Cập nhật thông tin người dùng thành công", "success");
                    return RedirectToAction("Index", "UserAccount");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }
       // 4. Yêu cầu xóa người dùng
        public ActionResult Delete(int id)
        {
            new UserAccountDAO().Delete(id);
            return RedirectToAction("Index");
        }

    }
}