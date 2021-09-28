using Data.Models;
using DevExtreme.NETCore.Demos.Models;
using Repository.Interfaces;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<ViewControl> ViewControl { get; }
        IRepository<AccessPermission> AccessPermission { get; }
        IRepository<ActionPermission> ActionPermission { get; }
        IRepository<User> User { get; }
        IRepository<Role> Role { get; }
        IRepository<ActionControl> ActionControl { get; }
        IRepository<Appointment> Appointment { get; }
        IRepository<Attachment> Attachment { get; }
        IRepository<Bed> Bed { get; }
        IRepository<BedAllocation> BedAllocation { get; }
        IRepository<Branch> Branch { get; }
        IRepository<Building> Building { get; }
        IRepository<City> City { get; }
        IRepository<Country> Country { get; }
        IRepository<Floor> Floor { get; }
        IRepository<Lookup> Lookup { get; }
        IRepository<PatientAdmission> PatientAdmission { get; }
        IRepository<PatientRelative> PatientRelative { get; }
        IRepository<PersonalInformation> PersonalInformation { get; }
        IRepository<PatientPersonalInformation> PatientPersonalInformation { get; }
        IRepository<Room> Room { get; }
        IRepository<MedicalHistory> MedicalHistory { get; }
        IRepository<LogTable> LogTable { get; }
        IRepository<LogData> LogData { get; }
        IRepository<EducationLevel> EducationLevel { get; }
        IRepository<PatientEncounter> PatientEncounter { get; }
        IRepository<IMDetailLookup> IMDetailLookup { get; }
        IRepository<PatientPreAssesment> PatientPreAssesment { get; }
        IRepository<PatientPreAssesmentVital> PatientPreAssesmentVital { get; }
        IRepository<PreAssessmentVitalDetail> PreAssessmentVitalDetail { get; }
        IRepository<InitialMedicalAssessment> InitialMedicalAssessment { get; }
        IRepository<InitialMedicalAssessmentDetail> InitialMedicalAssessmentDetail { get; }
        IRepository<CompanyProfile> CompanyProfile { get; }
        IRepository<CustImage> CustImage { get; }
        IRepository<Email> Email { get; }
        IRepository<EmailTrigger> EmailTrigger { get; }
        IRepository<SMS> SMS { get; }
        IRepository<SMSTrigger> SMSTrigger { get; }
        IRepository<SMSGroup> SMSGroup { get; }
        IRepository<GroupClientList> GroupClientList { get; }
        IRepository<SMSConfig> SMSConfig { get; }
        IRepository<EpilepsyTemplate> EpilepsyTemplate { get; }
        IRepository<MetabolismTemplate> MetabolismTemplate { get; }
        IRepository<EffectedGeneMetabolism> EffectedGeneMetabolism { get; }
        IRepository<FitnessTemplate> FitnessTemplate { get; }
        IRepository<EffectedGeneFitness> EffectedGeneFitness { get; }
        IRepository<FatConsumptionTemplate> FatConsumptionTemplate { get; }
        IRepository<MineralTemplate> MineralTemplate { get; }
        IRepository<EffectedGeneMineral> EffectedGeneMineral { get; }
        IRepository<MethyationTemplate> MethyationTemplate { get; }
        IRepository<DietTemplate> DietTemplate { get; }
        IRepository<EffectedGeneDiet> EffectedGeneDiet { get; }
        IRepository<ProstateTemplate> ProstateTemplate { get; }
        IRepository<VitaminTemplate> VitaminTemplate { get; }
        IRepository<EffectedGeneVitamin> EffectedGeneVitamin { get; }
        IRepository<PatientOrder> PatientOrder { get; }
        IRepository<PatientOrderProstate> PatientOrderProstate { get; }
        IRepository<PatientOrderMethyation> PatientOrderMethyation { get; }
        IRepository<PatientOrderFatConsumption> PatientOrderFatConsumption { get; }
        IRepository<PatientOrderEpilepsy> PatientOrderEpilepsy { get; }
        IRepository<PatientOrderMineral> PatientOrderMineral { get; }
        IRepository<PatientOrderFitness> PatientOrderFitness { get; }
        IRepository<PatientOrderDiet> PatientOrderDiet { get; }
        IRepository<PatientOrderVitamin> PatientOrderVitamin { get; }
        IRepository<PatientOrderMetabolism> PatientOrderMetabolism { get; }
        IRepository<PatientOrderEffectedGeneMetabolism> PatientOrderEffectedGeneMetabolism { get; }
        IRepository<PatientOrderEffectedGeneMineral> PatientOrderEffectedGeneMineral { get; }
        IRepository<PatientOrderEffectedGeneVitamin> PatientOrderEffectedGeneVitamin { get; }
        IRepository<PatientOrderEffectedGeneDiet> PatientOrderEffectedGeneDiet { get; }
        IRepository<PatientOrderEffectedGeneFitness> PatientOrderEffectedGeneFitness { get; }
        IRepository<Template> Template { get; }
        IRepository<TemplateColumn> TemplateColumn {get;}
        IRepository<TemplateData> TemplateData { get; }
        IRepository<ClientOrder> ClientOrder { get; }
        IRepository<ClientOrderData> ClientOrderData { get; }
        IRepository<MedicalDictionary> MedicalDictionary { get; }
        IRepository<MedicalDictionarySpecialty> MedicalDictionarySpecialty { get; }

        IRepository<TestTemplate> TestTemplate { get; }
        IRepository<TestTemplateColumn> TestTemplateColumn { get; }
        IRepository<TestTemplateData> TestTemplateData { get; }

        IRepository<TestTemp> TestTemp { get; }
        IRepository<MasterTempCol> MasterTempCol { get; }
        IRepository<TestTempCol> TestTempCol { get; }
        IRepository<TestTempData> TestTempData { get; }
        IRepository<Reminder> Reminder { get; }
        IRepository<FollowUpByDoc> FollowUpByDoc { get; }
        IRepository<FollowUpByDocConv> FollowUpByDocConv { get; }
        IRepository<FollowUpByDocResult> FollowUpByDocResult { get; }
        IRepository<FollowUpTestTempData> FollowUpTestTempData { get; }
        IRepository<ClientOrderTest> ClientOrderTest { get; }
        IRepository<AppointmentExp> AppointmentExp { get; }
        IRepository<TempDependency> TempDependency { get; }
        IRepository<TempDropDownDependency> TempDropDownDependency { get; }
    }
}
