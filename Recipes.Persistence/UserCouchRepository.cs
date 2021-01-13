namespace Recipes.Persistence
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.Extensions.Options;
    using MyCouch;
    using MyCouch.Requests;
    using Newtonsoft.Json.Linq;

    public class UserCouchRepository : IUserRepository
    {
        private LanguageService languageService;
        private MyCouchStore store;

        public UserCouchRepository(IOptions<ConnectionStrings> connectionStrings,
            LanguageService languageService)
        {
            this.languageService = languageService;
            this.store = new MyCouchStore(connectionStrings.Value.CouchDb, "recipes");
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var query = new Query("users", "users")
            {
                IncludeDocs = true
            };
            var users = await this.store.QueryAsync<string, User>(query);
            return users.Select(x => x.IncludedDoc);
        }

        public async Task<User> GetUserById(string id)
        {
            return await this.store.GetByIdAsync<User>(id);
        }

        public async Task<User> GetuserByUsername(string username)
        {
            var selector = new JObject
            {
                {"username", username}
            };

            var request = new FindRequest()
                .Configure(x => x.SelectorExpression(selector.ToString()));

            var response = await this.store.Client.Queries.FindAsync<User>(request);

            return response.Docs?[0];
        }
    }
}