using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelEF.DAO;
using ModelEF.Model;
using TestUngDung.Common;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // 1. Yêu cầu load ra trang Index
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
           // SetViewBag();
            return View(model);
        }
        //2. Yêu cầu tạo mới sản phẩm
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product pro)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                if (dao.GetByName(pro.Name) != null)
                {
                    SetAlert("Tên sản phẩm này đã tồn tại", "warning");
                    return RedirectToAction("Create", "Product");
                }
                long id = dao.Insert(pro);
                //Nếu insert thành công tức id >0
                if (id != 0)
                {
                    SetAlert("Thêm sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Thêm sản phẩm không thành công", "error");
                    return RedirectToAction("Index", "Product");
                }
            }
            return View("Index");
        }
        //Yêu cầu hiện droplist
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDAO();
            ViewBag.ProductType = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        //3. Yêu cầu xem chi tiết sản phẩm
        public ActionResult Detail(int id)
        {
            var detailView = new ProductDAO().Find(id);
            return View(detailView);
        }
        //4. Yêu cầu cập nhật sản phẩm
        public ActionResult Edit(int id)
        {
            var cat = new ProductDAO().ViewDetail(id);
            SetViewBag(cat.ProductType);
            return View(cat);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product pro)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();

                var result = dao.Update(pro);

                if (result)
                {
                    SetAlert("Cập nhật sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công");
                }
            }
            SetViewBag(pro.ProductType);
            return View("Index");
        }
        //5. Yêu cầu xóa sản phẩm
        public ActionResult Delete(int id)
        {
            new ProductDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}