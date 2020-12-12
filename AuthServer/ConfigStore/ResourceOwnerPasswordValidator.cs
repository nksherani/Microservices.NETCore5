//using IdentityModel;
//using IdentityServer4.Models;
//using IdentityServer4.Validation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace AuthServer.ConfigStore
//{
//    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
//    {
//        private readonly ConfigurationStoreContext dbcontext;

//        //repository to get user from db
//        //private readonly IUserRepository _userRepository;

//        public ResourceOwnerPasswordValidator(ConfigurationStoreContext dbcontext)
//        {
//            //_userRepository = userRepository; //DI
//            this.dbcontext = dbcontext;
//        }

//        //this is used to validate your user account with provided grant at /connect/token
//        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
//        {
//            try
//            {
//                //get your user model from db (by username - in my case its email)
//                //var user = await _userRepository.FindAsync(context.UserName);
//                var user = await dbcontext.Users.FindAsync(context.UserName);
//                //var user = dbcontext.Users.Where(x => x.UserName == context.UserName).FirstOrDefault();
//                if (user != null)
//                {
//                    //check if password match - remember to hash password if stored as hash in db
//                    if (user.PasswordHash == context.Password)
//                    {
//                        //set the result
//                        context.Result = new GrantValidationResult(
//                            subject: user.Id.ToString(),
//                            authenticationMethod: "custom",
//                            claims: GetUserClaims(user,dbcontext));

//                        return;
//                    }

//                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
//                    return;
//                }
//                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
//                return;
//            }
//            catch (Exception ex)
//            {
//                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
//            }
//        }

//        //build claims array from user data
//        public static Claim[] GetUserClaims(ApplicationUser user, ConfigurationStoreContext dbcontext)
//        {
//            var roleIds = dbcontext.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId);
//            var roles = dbcontext.Roles.Where(x => roleIds.Contains(x.Id));
//            List<Claim> claims = new List<Claim>();
//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(JwtClaimTypes.Role, role.Name));
//            }

//            claims.AddRange(new Claim[]
//            {
//            new Claim("user_id", user.UserName.ToString() ?? ""),
//            new Claim(JwtClaimTypes.Name, user.UserName),
//            new Claim(JwtClaimTypes.GivenName, user.UserName  ?? ""),
//            new Claim(JwtClaimTypes.FamilyName, user.UserName  ?? ""),
//            new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
//            new Claim("some_claim_you_want_to_see", "")
//            }
//            );

//            return claims.ToArray();
//        }
//    }
//}
