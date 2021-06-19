using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using ModelEF.Model;

namespace ModelEF.DAO
{
    public  class ProductDAO
    {
        NguyenVuAnhKhoaContext db = null;

        public ProductDAO()
        {
            db = new NguyenVuAnhKhoaContext();
        }
        //1. Phương thức trả về danh sách tất cả các danh mục
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
                model = model.Where(x => x.Name.Contains(searchString));
            return model.OrderBy(x => x.Quantity).ThenByDescending(x => x.UnitCost).ToPagedList(page, pageSize);
        }

        //2. Phương thức thêm mới sản phẩm
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        //3. Phương thức lấy ra tên sản phẩm
        public Product GetByName(string name)
        {
            return db.Products.SingleOrDefault(x => x.Name == name);
        }
        //3. Phương thức cập nhật thông tin sản phẩm
        public bool Update(Product entity)
        {
            try
            {
                var pro = db.Products.Find(entity.ID);
                pro.Name = entity.Name;
                pro.UnitCost = entity.UnitCost;
                pro.Quantity = entity.Quantity;
                pro.Image = entity.Image;
                pro.Description = entity.Description;
                pro.Status = entity.Status;
                pro.ProductType = entity.ProductType;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //4. Phương thức hiện lấy Id để cập nhật thông tin sản phẩm
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
        //5. Phương thức tìm sản phẩm theo id để xem chi tiết sản phẩm
        public Product Find(long id)
        {
            return db.Products.Find(id);
        }
        public List<Product> ListAll()
        {
            return db.Products.ToList();
        }
        //5. Phương thức xóa sản phẩm
        public bool Delete(int id)
        {
            try
            {
                var pro = db.Products.Find(id);
                db.Products.Remove(pro);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
       

    }
}
