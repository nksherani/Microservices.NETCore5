//using IdentityServer4.Models;
//using IdentityServer4.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AuthServer.ConfigStore
//{
//    public class ProfileService : IProfileService
//    {
//        //services
//        //private readonly IUserRepository _userRepository;
//        private readonly ConfigurationStoreContext dbcontext;

//        public ProfileService(ConfigurationStoreContext dbcontext)
//        {
//            //_userRepository = userRepository;
//            this.dbcontext = dbcontext;
//        }

//        //Get user profile date in terms of claims when calling /connect/userinfo
//        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
//        {
//            try
//            {
//                //depending on the scope accessing the user data.
//                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
//                {
//                    //get user from db (in my case this is by email)
//                    var user = await dbcontext.Users.FindAsync(context.Subject.Identity.Name);

//                    if (user != null)
//                    {
//                        var claims = ResourceOwnerPasswordValidator.GetUserClaims(user,dbcontext);

//                        //set issued claims to return
//                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
//                    }
//                }
//                else
//                {
//                    //get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
//                    //where and subject was set to my user id.
//                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

//                    if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
//                    {
//                        //get user from db (find user by user id)
//                        var user = await dbcontext.Users.FindAsync(long.Parse(userId.Value));

//                        // issue the claims for the user
//                        if (user != null)
//                        {
//                            var claims = ResourceOwnerPasswordValidator.GetUserClaims(user,dbcontext);

//                            context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //log your error
//            }
//        }

//        //check if user account is active.
//        public async Task IsActiveAsync(IsActiveContext context)
//        {
//            try
//            {
//                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
//                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

//                if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
//                {
//                    var user = await dbcontext.Users.FindAsync(long.Parse(userId.Value));

//                    if (user != null)
//                    {
//                        //if (user.EmailConfirmed)
//                        {
//                            //context.IsActive = user.IsActive;
//                            context.IsActive = true;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //handle error logging
//            }
//        }
//    }
//}
