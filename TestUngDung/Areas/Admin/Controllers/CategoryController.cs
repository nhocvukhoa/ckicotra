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
    public class CategoryController : BaseController
    {
        //1. Yêu cầu load ra trang quản lý
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new CategoryDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        //2. Yêu cầu Thêm mới danh mục
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                if (dao.GetByName(cat.Name) != null)
                {
                    SetAlert("Tên danh mục này đã tồn tại", "warning");
                    return RedirectToAction("Create", "Category");
                }
                long id = dao.Insert(cat);
                //Nếu insert thành công tức id >0
                if (id != 0)
                {
                    SetAlert("Thêm danh mục thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Thêm danh mục không thành công", "error");
                }
              
            }
            return View("Index");
        }
        //3. Yêu cầu Cập nhật danh mục
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cat = new CategoryDAO().ViewDetail(id);
            return View(cat);
        }
        [HttpPost]
        public ActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                var result = dao.Update(cat);
                if (result)
                {
                    SetAlert("Cập nhật thông tin danh mục", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }
        //4. Yêu cầu Xóa danh mục
        public ActionResult Delete(int id)
        {
            new CategoryDAO().Delete(id);
            return RedirectToAction("Index");
        }
      

    }
}