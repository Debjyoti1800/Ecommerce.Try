using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository
    {
        #region Constructor
        private UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region GetAllUsers
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            try
            {
                users = _context.Users.ToList();
            }
            catch (Exception)
            {
                users = [];
            }

            return users;
        }
        #endregion

        #region GetUserByEmail
        public User GetUserByEmail(string email)
        {
            User user = null;
            try
            {
                user = _context.Users.FirstOrDefault(u => u.UserEmail == email);
            }
            catch (Exception)
            {
                user = null;
            }
            return user;
        }
        #endregion

        #region Create user
        public bool CreateUser(User user)
        {
            bool status = false;

            try
            {
                status = _context.Users.Add(user) != null;
                _context.SaveChanges();

            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region UpdateUser
        public bool UpdateUser(User user)
        {
            bool status = false;
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region DeleteUser
        public bool DeleteUser(string email)
        {
            bool status = false;
            try
            {
                var temp = _context.Users.FirstOrDefault(u => u.UserEmail == email);
                if(temp != null)
                {
                    _context.Users.Remove(temp);
                    _context.SaveChanges();
                    status = true;
                }
                
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #endregion

    }
}
