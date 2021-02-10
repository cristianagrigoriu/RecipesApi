namespace Recipes.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(string id);

        Task<User> GetUserByUsername(string username);

        void AddUser(User newUser);
        Task<User> UpdateUser(User userToUpdate);
    }
}