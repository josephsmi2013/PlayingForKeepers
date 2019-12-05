using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PlayingForKeepers.Models.DB.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PlayingForKeepers.Models.DB
{
    public partial class PlayingForKeepersDbContext : IdentityDbContext<ApplicationUser>
    {

        #region public properties
        #endregion



        #region default constructor method
        public PlayingForKeepersDbContext()
        {
        }
        #endregion



        #region overloaded constructor method
        public PlayingForKeepersDbContext(DbContextOptions<PlayingForKeepersDbContext> options)
            : base(options)
        {
        }
        #endregion



        #region OnModelCreating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FF_LeagueActivity>().HasKey("LeagueActivityID");
            modelBuilder.Entity<FF_Leagues>().HasKey("LeagueID");
            modelBuilder.Entity<FF_LeagueUsers>().HasNoKey();            
            modelBuilder.Entity<FF_Teams>().HasKey("TeamID");

        }
        #endregion



        #region ExecuteSP method  
        // Executes a DB stored proc and returns a bool to the caller
        public Task<bool> ExecuteSP(string SPName, params object[] paramArray)
        {

            ////Initialize
            List<SqlParameter> cParameters = new List<SqlParameter>();
            bool outputParamValue;
            string sqlParams = "";
            string sqlQuery;


            try
            {

                for (int i = 0; i < paramArray.Length; i++)
                {
                    var inputParam = paramArray[i];
                    string paramName = "@p" + i;

                    cParameters.Add(new SqlParameter(paramName, inputParam.ToString()));

                    sqlParams += paramName + ",";

                }

                sqlParams += "@OutParam OUT";
                sqlQuery = "EXEC [dbo].[" + SPName + "] " + sqlParams;

                SqlParameter outputParam1 = new SqlParameter("@OutParam", SqlDbType.Bit);
                outputParam1.Direction = ParameterDirection.Output;
                cParameters.Add(outputParam1);

                Database.ExecuteSqlRaw(sqlQuery, cParameters);
                outputParamValue = (bool)outputParam1.Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Task.FromResult(outputParamValue);


        }
        #endregion



        #region GeJoinedLeaguesAsync SP method
        // Gets a list of leagues the user has joined and returns to the caller
        public async Task<List<FF_Leagues>> GetJoinedLeaguesAsync(string userId)
        {
            // Initialization.  
            List<FF_Leagues> lst = new List<FF_Leagues>();

            try
            {
                // Processing.  
                SqlParameter inputParam1 = new SqlParameter("@UserId", userId);
                string sqlQuery = "EXEC [dbo].[FF_GetJoinedLeagues] " + "@UserId";

                lst = await this.Set<FF_Leagues>().FromSqlRaw(sqlQuery, inputParam1).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }
        #endregion 



        #region GetLeaguesAsync SP method
        // Gets a list of leagues and returns to the caller. LeagueId can be null
        public async Task<List<FF_Leagues>> GetLeaguesAsync(int leagueId = 0)
        {
            // Initialization.  
            List<FF_Leagues> lst = new List<FF_Leagues>();

            try
            {
                // Processing.
                if (leagueId == 0)
                {
                    string sqlQuery = "EXEC [dbo].[FF_GetLeagues] ";
                    lst = await this.Set<FF_Leagues>().FromSqlRaw(sqlQuery).ToListAsync();
                }
                else
                {
                    SqlParameter inputParam1 = new SqlParameter("@LeagueId", leagueId.ToString());
                    string sqlQuery = "EXEC [dbo].[FF_GetLeagues] " + "@LeagueId";
                    lst = await this.Set<FF_Leagues>().FromSqlRaw(sqlQuery, inputParam1).ToListAsync();
                }

     
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }
        #endregion     



        #region GetLeagueActivityAsync SP method
        // Gets a list of league activity and returns to the caller
        public async Task<List<FF_LeagueActivity>> GetLeagueActivityAsync(int leagueId)
        {
            // Initialization.  
            List<FF_LeagueActivity> lst = new List<FF_LeagueActivity>();

            try
            {
                // Processing. 
                SqlParameter inputParam1 = new SqlParameter("@LeagueId", leagueId.ToString());
                string sqlQuery = "EXEC [dbo].[FF_GetLeagueActivity] " + "@LeagueId";

                lst = await this.Set<FF_LeagueActivity>().FromSqlRaw(sqlQuery, inputParam1).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }
        #endregion



        #region GetTeamsAsync SP method
        // Gets a list of teams and returns to the caller
        public async Task<List<FF_Teams>> GetTeamsAsync(int leagueId = 0)
        {
            // Initialization.  
            List<FF_Teams> lst = new List<FF_Teams>();

            try
            {
                // Processing.
                if (leagueId == 0)
                {
                    string sqlQuery = "EXEC [dbo].[FF_GetTeams] ";
                    lst = await this.Set<FF_Teams>().FromSqlRaw(sqlQuery).ToListAsync();
                }
                else
                {
                    SqlParameter inputParam1 = new SqlParameter("@LeagueId", leagueId.ToString());
                    string sqlQuery = "EXEC [dbo].[FF_GetTeams] " + "@LeagueId";
                    lst = await this.Set<FF_Teams>().FromSqlRaw(sqlQuery, inputParam1).ToListAsync();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }
        #endregion

    }
}
