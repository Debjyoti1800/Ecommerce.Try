using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Repository;

namespace UserService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        #region Constructor
        private UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Methods

        #region GetAllUsers
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<Models.User> userLst = new List<Models.User>();

            try
            {
                userLst = _userRepository.GetAllUsers();
            }
            catch (Exception)
            {

                userLst = [];
            }
            
            return Json(userLst);
        }
        #endregion

        #region GetUserByEmail
        [HttpGet("{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            Models.User user = null;
            try
            {
                user = _userRepository.GetUserByEmail(email);
            }
            catch (Exception)
            {
                user = null;
            }
            return Json(user);
        }
        #endregion

        #region CreateUser
        [HttpPost]
        public IActionResult CreateUser(Models.User user)
        {
            bool status = false;
            try
            {
                status = _userRepository.CreateUser(user);
            }
            catch (Exception)
            {
                status = false;
            }
            return Json(status);
        }
        #endregion

        #region UpdateUser
        [HttpPut]
        public IActionResult UpdateUser(Models.User user)
        {
            bool status = false;
            try
            {
                status = _userRepository.UpdateUser(user);
            }
            catch (Exception)
            {
                status = false;
            }
            return Json(status);
        }
        #endregion

        #region DeleteUser
        [HttpDelete("{email}")]
        public IActionResult DeleteUser(string email)
        {
            bool status = false;
            try
            {
                status = _userRepository.DeleteUser(email);
            }
            catch (Exception)
            {
                status = false;
            }
            return Json(status);
        }
        #endregion


        #endregion

    }
}
