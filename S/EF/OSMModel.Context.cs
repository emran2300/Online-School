//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace S.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlineEduEntities : DbContext
    {
        public OnlineEduEntities()
            : base("name=OnlineEduEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<LoginMatch> LoginMatches { get; set; }
        public virtual DbSet<CourseStudentMap> CourseStudentMaps { get; set; }
    }
}
