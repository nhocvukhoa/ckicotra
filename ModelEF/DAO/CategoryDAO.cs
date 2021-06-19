using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace ModelEF.DAO
{
    public class CategoryDAO
    {
        NguyenVuAnhKhoaContext db = null;
        
        public CategoryDAO()
        {
            db = new NguyenVuAnhKhoaContext();
        }
        //Lấy ra list danh mục để tạo drop cho loại sản phẩm bên bảng spham
        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }
        //1. Phương thức thêm mới danh mục
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        //2. Phương thức lấy ra tên danh mục
        public Category GetByName(string name)
        {
            return db.Categories.SingleOrDefault(x => x.Name == name);
        }
        //3. Phương thức cập nhật thông tin danh mục
        public bool Update(Category entity)
        {
            try
            {
                var cat = db.Categories.Find(entity.ID);
                cat.Name = entity.Name;
                cat.Description = entity.Description;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //4. Phương thức hiện lấy Id để cập nhật thông tin danh mục
        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }
        //5. Phương thức xóa danh mục
        public bool Delete(int id)
        {
            try
            {
                var cat = db.Categories.Find(id);
                db.Categories.Remove(cat);
                db.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        //. Phương thức trả về danh sách tất cả các danh mục
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
                model = model.Where(x => x.Name.Contains(searchString));
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}
