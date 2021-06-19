using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestUngDung.Common
{
    //Chuyển đổi đối tượng về dạng trung gian phục vụ lưu trữ
    [Serializable]
    public class UserLogin
    {
        public long UserID { get; set; }
        public string Username { get; set;}
    }
}