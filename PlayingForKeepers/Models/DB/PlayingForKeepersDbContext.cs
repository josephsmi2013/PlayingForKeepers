using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PlayingForKeepers.Models.DB.Tables;
using PlayingForKeepers.Models.DB.Stored_Procs;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PlayingForKeepers.Models.DB
{
    public partial class PlayingForKeepersDbContext : IdentityDbContext
    {


        public virtual DbSet<FF_Leagues> FF_Leagues { get; set; }


        public PlayingForKeepersDbContext()
        {
        }

        public PlayingForKeepersDbContext(DbContextOptions<PlayingForKeepersDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //Register the custom tables
            modelBuilder.Entity<FF_Leagues>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LeagueID).ValueGeneratedOnAdd();

                entity.Property(e => e.LeagueName)
                    .IsRequired()
                    .HasMaxLength(64);
            });
           


            //Register the custom stored procedures
            modelBuilder.Entity<FF_GetLeagues>().HasNoKey();
            modelBuilder.Entity<FF_AddLeague>().HasNoKey();
        }

        

        #region Get league store procedure method
        // Gets a list of leagues and returns to the caller
        public async Task<List<FF_GetLeagues>> GetLeaguesAsync()
        {
            // Initialization.  
            List<FF_GetLeagues> lst = new List<FF_GetLeagues>();

            try
            {
                // Processing.  
                string sqlQuery = "EXEC [dbo].[FF_GetLeagues] ";

                lst = await this.Set<FF_GetLeagues>().FromSqlRaw(sqlQuery).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }
        #endregion



        #region Add league stored procedure method  
        // Adds a league and returns a bool to the caller 
        public Task<bool> AddLeague(string leagueName, int leagueTeamsPossible, string leagueOwnerID, LeagueStatusListSP leagueStatus)
        {

            //Initialize
            bool outputParamValue;

            try
            {
                SqlParameter inputParam1 = new SqlParameter("@LeagueName", leagueName);
                SqlParameter inputParam2 = new SqlParameter("@LeagueTeamsPossible", leagueTeamsPossible);
                SqlParameter inputParam3 = new SqlParameter("@LeagueOwnerID", leagueOwnerID);
                SqlParameter inputParam4 = new SqlParameter("@LeagueStatus", leagueStatus);

                SqlParameter outputParam1 = new SqlParameter("@OutParam", SqlDbType.Bit);
                outputParam1.Direction = ParameterDirection.Output;               
                 
                string sqlQuery = "EXEC [dbo].[FF_AddLeague] " + "@LeagueName" + "," + "@LeagueTeamsPossible" + "," + "@LeagueOwnerID" + "," + "@LeagueStatus" + "," + "@OutParam OUT";

                Database.ExecuteSqlRaw(sqlQuery, inputParam1, inputParam2, inputParam3, inputParam4, outputParam1);
                outputParamValue = (bool)outputParam1.Value;
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Task.FromResult(outputParamValue);
        }
        #endregion


    }
}
