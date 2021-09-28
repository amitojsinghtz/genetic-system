using Data.Mapping;
using Data.Models;
using DevExtreme.NETCore.Demos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Server=192.168.1.34;Database=genetic_db;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer(@"Data Source = 192.168.0.34; Initial Catalog = genetic_db; User ID = sa; Password = techno@123");
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Server=sql6009.site4now.net;Database=DB_A56ED5_capsys;user id=DB_A56ED5_capsys_admin;password=ffEKfuH334;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Server=sql6009.site4now.net;Database=db_a3cd7d_genetic2019;user id=DB_A3CD7D_genetic2019_admin;password=Israrassi@1988;MultipleActiveResultSets=true");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TempDependency>()
            .HasOne(m => m.Col)
            .WithOne(t => t.ColIDTempDependencies)
            .HasForeignKey<TempDependency>(m => m.ColID);

            modelBuilder.Entity<TempDependency>()
                        .HasOne(m => m.ChkBox)
                        .WithMany(t => t.ChkBoxTempDependencies)
                        .HasForeignKey(m => m.ChkBoxID);

            modelBuilder.Entity<TemplateColumn>()
                .HasMany(t => t.ChkBoxTempDependencies)
                .WithOne(m => m.ChkBox)
                .HasForeignKey(m => m.ChkBoxID);

            modelBuilder.Entity<TemplateColumn>()
                .HasOne(t => t.ColIDTempDependencies)
                .WithOne(m => m.Col)
                .HasForeignKey<TempDependency>(m => m.ColID);

            modelBuilder.Entity<TempDropDownDependency>()
                .HasOne(m => m.Col)
                .WithOne(t => t.ColIDDropDownDependency)
                .HasForeignKey<TempDropDownDependency>(m => m.ColID);

            modelBuilder.Entity<TempDropDownDependency>()
                        .HasOne(m => m.DropDown)
                        .WithMany(t => t.DropDownDependencies)
                        .HasForeignKey(m => m.DropDownId);

            modelBuilder.Entity<TemplateColumn>()
             .HasMany(t => t.DropDownDependencies)
             .WithOne(m => m.DropDown)
             .HasForeignKey(m => m.DropDownId);

            modelBuilder.Entity<TemplateColumn>()
                .HasOne(t => t.ColIDDropDownDependency)
                .WithOne(m => m.Col)
                .HasForeignKey<TempDropDownDependency>(m => m.ColID);
        }

        public DbSet<User> User { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Floor> Floor { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Bed> Bed { get; set; }
        public DbSet<BedAllocation> BedAllocation { get; set; }
        public DbSet<PatientAdmission> PatientAdmission { get; set; }
        public DbSet<PatientRelative> PatientRelative { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<ViewControl> ViewControl { get; set; }
        public DbSet<AccessPermission> AccessPermission { get; set; }
        public DbSet<ActionControl> ActionControl { get; set; }
        public DbSet<ActionPermission> ActionPermission { get; set; }
        public DbSet<PatientPersonalInformation> PatientPersonalInformation { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<LogTable> LogTable { get; set; }
        public DbSet<LogData> LogData { get; set; }
        public DbSet<IMDetailLookup> IMDetailLookup { get; set; }
        public DbSet<PatientEncounter> PatientEncounter { get; set; }
        public DbSet<PatientPreAssesment> PatientPreAssesment { get; set; }
        public DbSet<PatientPreAssesmentVital> PatientPreAssesmentVital { get; set; }
        public DbSet<PreAssessmentVitalDetail> PreAssessmentVitalDetail { get; set; }
        public DbSet<InitialMedicalAssessment> InitialMedicalAssessment { get; set; }
        public DbSet<InitialMedicalAssessmentDetail> InitialMedicalAssessmentDetail { get; set; }
        public DbSet<CompanyProfile> CompanyProfile { get; set; }
        public DbSet<CustImage> CustImage { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<EmailTrigger> EmailTrigger { get; set; }
        public DbSet<SMS> SMS { get; set; }
        public DbSet<SMSTrigger> SMSTrigger { get; set; }
        public DbSet<SMSGroup> SMSGroup { get; set; }
        public DbSet<GroupClientList> GroupClientList { get; set; }
        public DbSet<SMSConfig> SMSConfig { get; set; }
        public DbSet<EpilepsyTemplate> EpilepsyTemplate { get; set; }
        public DbSet<MetabolismTemplate> MetabolismTemplate { get; set; }
        public DbSet<EffectedGeneMetabolism> EffectedGeneMetabolism { get; set; }
        public DbSet<FitnessTemplate> FitnessTemplate { get; set; }
        public DbSet<EffectedGeneFitness> EffectedGeneFitness { get; set; }
        public DbSet<FatConsumptionTemplate> FatConsumptionTemplate { get; set; }
        public DbSet<MineralTemplate> MineralTemplate { get; set; }
        public DbSet<EffectedGeneMineral> EffectedGeneMineral { get; set; }
        public DbSet<MethyationTemplate> MethyationTemplate { get; set; }
        public DbSet<DietTemplate> DietTemplate { get; set; }
        public DbSet<EffectedGeneDiet> EffectedGeneDiet { get; set; }
        public DbSet<ProstateTemplate> ProstateTemplate { get; set; }
        public DbSet<VitaminTemplate> VitaminTemplate { get; set; }
        public DbSet<EffectedGeneVitamin> EffectedGeneVitamin { get; set; }
        public DbSet<PatientOrder> PatientOrder { get; set; }
        public DbSet<PatientOrderDiet> PatientOrderDiet { get; set; }
        public DbSet<PatientOrderEffectedGeneDiet> PatientOrderEffectedGeneDiet { get; set; }
        public DbSet<PatientOrderEffectedGeneFitness> PatientOrderEffectedGeneFitness { get; set; }
        public DbSet<PatientOrderEffectedGeneMetabolism> PatientOrderEffectedGeneMetabolism { get; set; }
        public DbSet<PatientOrderEffectedGeneMineral> PatientOrderEffectedGeneMineral { get; set; }

        public DbSet<PatientOrderFitness> PatientOrderFitness { get; set; }
        public DbSet<PatientOrderMineral> PatientOrderMineral { get; set; }
        public DbSet<PatientOrderMetabolism> PatientOrderMetabolism { get; set; }
        public DbSet<PatientOrderVitamin> PatientOrderVitamin { get; set; }

        public DbSet<PatientOrderEpilepsy> PatientOrderEpilepsy { get; set; }
        public DbSet<PatientOrderFatConsumption> PatientOrderFatConsumption { get; set; }
        public DbSet<PatientOrderMethyation> PatientOrderMethyation { get; set; }
        public DbSet<PatientOrderProstate> PatientOrderProstate { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<TemplateColumn> TemplateColumn { get; set; }
        public DbSet<TemplateData> TemplateData { get; set; }
        public DbSet<ClientOrder> ClientOrder { get; set; }
        public DbSet<ClientOrderData> ClientOrderData { get; set; }
        public DbSet<MedicalDictionary> MedicalDictionary { get; set; }
        public DbSet<MedicalDictionarySpecialty> MedicalDictionarySpecialty { get; set; }


        public DbSet<TestTemplate> TestTemplate { get; set; }
        public DbSet<TestTemplateColumn> TestTemplateColumn { get; set; }
        public DbSet<TestTemplateData> TestTemplateData { get; set; }
        public DbSet<TestTemp> TestTemp { get; set; }
        public DbSet<TestTempCol> TestTempCol { get; set; }
        public DbSet<MasterTempCol> MasterTempCol { get; set; }
        public DbSet<TestTempData> TestTempData { get; set; }
        public DbSet<Reminder> Reminder { get; set; }
        public DbSet<FollowUpByDoc> FollowUpByDoc { get; set; }
        public DbSet<FollowUpByDocConv> FollowUpByDocConv { get; set; }
        public DbSet<FollowUpByDocResult> FollowUpByDocResult { get; set; }
        public DbSet<FollowUpTestTempData> FollowUpTestTempData { get; set; }
        public DbSet<ClientOrderTest> ClientOrderTest { get; set; }
        public DbSet<AppointmentExp> AppointmentExp { get; set; }
        public DbSet<TempDependency> TempDependency { get; set; }
        public DbSet<TempDropDownDependency> TempDropDownDependency { get; set; }

    }
}
