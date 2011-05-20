using Db4objects.Db4o;
namespace Statis.Models
{
    public abstract class RegisteredUser : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public void AddUser(IObjectContainer db)
        {
            db.Store(this);
        }
        public void DeleteUser(IObjectContainer db)
        {
            db.Delete(this);
        }
    }
}