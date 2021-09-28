using Data.Models;
using DevExtreme.NETCore.Demos.Models;
using Repository.Classes;
using Repository.Interfaces;

namespace Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        Repository<EducationLevel> _EducationLevel;
        private ApplicationContext _context;
        private Repository<ViewControl> _viewControl;
        private Repository<AccessPermission> _accessPermission;
        private Repository<ActionPermission> _actionPermission;
        private Repository<User> _user;
        private Repository<Role> _role;
        Repository<ActionControl> _ActionControl;
        Repository<Appointment> _Appointment;
        Repository<Attachment> _Attachment;
        Repository<Bed> _Bed;
        Repository<BedAllocation> _BedAllocation;
        Repository<Branch> _Branch;
        Repository<Building> _Building;
        Repository<City> _City;
        Repository<Country> _Country;
        Repository<Floor> _Floor;
        Repository<Lookup> _Lookup;
        Repository<PatientAdmission> _PatientAdmission;
        Repository<PatientRelative> _PatientRelative;
        Repository<PersonalInformation> _PersonalInformation;
        Repository<Room> _Room;
        Repository<PatientPersonalInformation> _PatientPersonalInformation;
        Repository<MedicalHistory> _MedicalHistory;
        Repository<LogTable> _LogTable;
        Repository<LogData> _LogData;
        Repository<PatientEncounter> _PatientEncounter;
        Repository<IMDetailLookup> _IMDetailLookup;
        Repository<PatientPreAssesment> _PatientPreAssesment;
        Repository<PatientPreAssesmentVital> _PatientPreAssesmentVital;
        Repository<PreAssessmentVitalDetail> _PreAssessmentVitalDetail;
        Repository<InitialMedicalAssessment> _InitialMedicalAssessment;
        Repository<InitialMedicalAssessmentDetail> _InitialMedicalAssessmentDetail;
        Repository<CompanyProfile> _CompanyProfile;
        Repository<CustImage> _CustImage;
        Repository<Email> _Email;
        Repository<EmailTrigger> _EmailTrigger;
        Repository<SMS> _SMS;
        Repository<SMSTrigger> _SMSTrigger;
        Repository<SMSGroup> _SMSGroup;
        Repository<GroupClientList> _GroupClientList;
        Repository<SMSConfig> _SMSConfig;
        Repository<EpilepsyTemplate> _EpilepsyTemplate;
        Repository<MetabolismTemplate> _MetabolismTemplate;
        Repository<EffectedGeneMetabolism> _EffectedGeneMetabolism;
        Repository<FitnessTemplate> _FitnessTemplate;
        Repository<EffectedGeneFitness> _EffectedGeneFitness;
        Repository<FatConsumptionTemplate> _FatConsumptionTemplate;
        Repository<MineralTemplate> _MineralTemplate;
        Repository<EffectedGeneMineral> _EffectedGeneMineral;
        Repository<MethyationTemplate> _MethyationTemplate;
        Repository<DietTemplate> _DietTemplate;
        Repository<EffectedGeneDiet> _EffectedGeneDiet;
        Repository<ProstateTemplate> _ProstateTemplate;
        Repository<VitaminTemplate> _VitaminTemplate;
        Repository<EffectedGeneVitamin> _EffectedGeneVitamin;
        Repository<PatientOrder> _PatientOrder;
        Repository<PatientOrderEpilepsy> _PatientOrderEpilepsy;
        Repository<PatientOrderFatConsumption> _PatientOrderFatConsumption;
        Repository<PatientOrderMethyation> _PatientOrderMethyation;
        Repository<PatientOrderProstate> _PatientOrderProstate;
        Repository<PatientOrderMineral> _PatientOrderMineral;
        Repository<PatientOrderFitness> _PatientOrderFitness;
        Repository<PatientOrderDiet> _PatientOrderDiet;
        Repository<PatientOrderVitamin> _PatientOrderVitamin;
        Repository<PatientOrderMetabolism> _PatientOrderMetabolism;
        Repository<PatientOrderEffectedGeneMetabolism> _PatientOrderEffectedGeneMetabolism;
        Repository<PatientOrderEffectedGeneMineral> _PatientOrderEffectedGeneMineral;
        Repository<PatientOrderEffectedGeneVitamin> _PatientOrderEffectedGeneVitamin;
        Repository<PatientOrderEffectedGeneDiet> _PatientOrderEffectedGeneDiet;
        Repository<PatientOrderEffectedGeneFitness> _PatientOrderEffectedGeneFitness;
        Repository<Template> _Template;
        Repository<TemplateColumn> _TemplateColumn;
        Repository<TemplateData> _TemplateData;
        Repository<ClientOrder> _ClientOrder;
        Repository<ClientOrderData> _ClientOrderData;
        Repository<MedicalDictionary> _MedicalDictionary;
        Repository<MedicalDictionarySpecialty> _MedicalDictionarySpecialty;

        Repository<TestTemplate> _TestTemplate;
        Repository<TestTemplateColumn> _TestTemplateColumn;
        Repository<TestTemplateData> _TestTemplateData;

        Repository<TestTemp> _TestTemp;
        Repository<MasterTempCol> _MasterTempCol;
        Repository<TestTempCol> _TestTempCol;
        Repository<TestTempData> _TestTempData;
        Repository<Reminder> _Reminder;
        Repository<FollowUpByDoc> _FollowUpByDoc;
        Repository<FollowUpByDocConv> _FollowUpByDocConv;
        Repository<FollowUpByDocResult> _FollowUpByDocResult;
        Repository<FollowUpTestTempData> _FollowUpTestTempData;
        Repository<ClientOrderTest> _ClientOrderTest;
        Repository<AppointmentExp> _AppointmentExp; 
        Repository<TempDependency> _TempDependency;
        Repository<TempDropDownDependency> _TempDropDownDependency;

        public UnitOfWork()
        {
            this._context = new ApplicationContext();
        
        }
        public IRepository<TempDropDownDependency> TempDropDownDependency
        {
            get
            {
                if (_TempDropDownDependency != null)
                {
                    return _TempDropDownDependency;
                }
                else
                {
                    _TempDropDownDependency = new Repository<TempDropDownDependency>(_context);
                    return _TempDropDownDependency;
                }

            }
        }
        public IRepository<TempDependency> TempDependency
        {
            get
            {
                if (_TempDependency != null)
                {
                    return _TempDependency;
                }
                else
                {
                    _TempDependency = new Repository<TempDependency>(_context);
                    return _TempDependency;
                }

            }
        }
        public IRepository<AppointmentExp> AppointmentExp
        {
            get
            {
                if (_AppointmentExp != null)
                {
                    return _AppointmentExp;
                }
                else
                {
                    _AppointmentExp = new Repository<AppointmentExp>(_context);
                    return _AppointmentExp;
                }

            }
        }
        public IRepository<ClientOrderTest> ClientOrderTest
        {
            get
            {
                if (_ClientOrderTest != null)
                {
                    return _ClientOrderTest;
                }
                else
                {
                    _ClientOrderTest = new Repository<ClientOrderTest>(_context);
                    return _ClientOrderTest;
                }

            }
        }
        public IRepository<FollowUpTestTempData> FollowUpTestTempData
        {
            get
            {
                if (_FollowUpTestTempData != null)
                {
                    return _FollowUpTestTempData;
                }
                else
                {
                    _FollowUpTestTempData = new Repository<FollowUpTestTempData>(_context);
                    return _FollowUpTestTempData;
                }

            }
        }
        public IRepository<FollowUpByDoc> FollowUpByDoc
        {
            get
            {
                if (_FollowUpByDoc != null)
                {
                    return _FollowUpByDoc;
                }
                else
                {
                    _FollowUpByDoc = new Repository<FollowUpByDoc>(_context);
                    return _FollowUpByDoc;
                }

            }
        }
        public IRepository<FollowUpByDocConv> FollowUpByDocConv
        {
            get
            {
                if (_FollowUpByDocConv != null)
                {
                    return _FollowUpByDocConv;
                }
                else
                {
                    _FollowUpByDocConv = new Repository<FollowUpByDocConv>(_context);
                    return _FollowUpByDocConv;
                }

            }
        }
        public IRepository<FollowUpByDocResult> FollowUpByDocResult
        {
            get
            {
                if (_FollowUpByDocResult != null)
                {
                    return _FollowUpByDocResult;
                }
                else
                {
                    _FollowUpByDocResult = new Repository<FollowUpByDocResult>(_context);
                    return _FollowUpByDocResult;
                }

            }
        }
        public IRepository<Reminder> Reminder
        {
            get
            {
                if (_Reminder != null)
                {
                    return _Reminder;
                }
                else
                {
                    _Reminder = new Repository<Reminder>(_context);
                    return _Reminder;
                }

            }
        }
        public IRepository<TestTempData> TestTempData
        {
            get
            {
                if (_TestTempData != null)
                {
                    return _TestTempData;
                }
                else
                {
                    _TestTempData = new Repository<TestTempData>(_context);
                    return _TestTempData;
                }

            }
        }


        public IRepository<TestTempCol> TestTempCol
        {
            get
            {
                if (_TestTempCol != null)
                {
                    return _TestTempCol;
                }
                else
                {
                    _TestTempCol = new Repository<TestTempCol>(_context);
                    return _TestTempCol;
                }

            }
        }

        public IRepository<TestTemp> TestTemp
        {
            get
            {
                if (_TestTemp != null)
                {
                    return _TestTemp;
                }
                else
                {
                    _TestTemp = new Repository<TestTemp>(_context);
                    return _TestTemp;
                }

            }
        }
        public IRepository<MasterTempCol> MasterTempCol
        {
            get
            {
                if (_MasterTempCol != null)
                {
                    return _MasterTempCol;
                }
                else
                {
                    _MasterTempCol = new Repository<MasterTempCol>(_context);
                    return _MasterTempCol;
                }

            }
        }

        public IRepository<TestTemplateData> TestTemplateData
        {
            get
            {
                if (_TestTemplateData != null)
                {
                    return _TestTemplateData;
                }
                else
                {
                    _TestTemplateData = new Repository<TestTemplateData>(_context);
                    return _TestTemplateData;
                }

            }
        }
        public IRepository<TestTemplateColumn> TestTemplateColumn
        {
            get
            {
                if (_TestTemplateColumn != null)
                {
                    return _TestTemplateColumn;
                }
                else
                {
                    _TestTemplateColumn = new Repository<TestTemplateColumn>(_context);
                    return _TestTemplateColumn;
                }

            }
        }
        public IRepository<TestTemplate> TestTemplate
        {
            get
            {
                if (_TestTemplate != null)
                {
                    return _TestTemplate;
                }
                else
                {
                    _TestTemplate = new Repository<TestTemplate>(_context);
                    return _TestTemplate;
                }

            }
        }



        public IRepository<MedicalDictionarySpecialty> MedicalDictionarySpecialty
        {
            get
            {
                if (_MedicalDictionarySpecialty != null)
                {
                    return _MedicalDictionarySpecialty;
                }
                else
                {
                    _MedicalDictionarySpecialty = new Repository<MedicalDictionarySpecialty>(_context);
                    return _MedicalDictionarySpecialty;
                }

            }
        }
        public IRepository<MedicalDictionary> MedicalDictionary
        {
            get
            {
                if (_MedicalDictionary != null)
                {
                    return _MedicalDictionary;
                }
                else
                {
                    _MedicalDictionary = new Repository<MedicalDictionary>(_context);
                    return _MedicalDictionary;
                }

            }
        }
        public IRepository<ClientOrderData> ClientOrderData
        {
            get
            {
                if (_ClientOrderData != null)
                {
                    return _ClientOrderData;
                }
                else
                {
                    _ClientOrderData = new Repository<ClientOrderData>(_context);
                    return _ClientOrderData;
                }

            }
        }
        public IRepository<ClientOrder> ClientOrder
        {
            get
            {
                if (_ClientOrder != null)
                {
                    return _ClientOrder;
                }
                else
                {
                    _ClientOrder = new Repository<ClientOrder>(_context);
                    return _ClientOrder;
                }

            }
        }
        public IRepository<TemplateData> TemplateData
        {
            get
            {
                if (_TemplateData != null)
                {
                    return _TemplateData;
                }
                else
                {
                    _TemplateData = new Repository<TemplateData>(_context);
                    return _TemplateData;
                }

            }
        }
        public IRepository<TemplateColumn> TemplateColumn
        {
            get
            {
                if (_TemplateColumn != null)
                {
                    return _TemplateColumn;
                }
                else
                {
                    _TemplateColumn = new Repository<TemplateColumn>(_context);
                    return _TemplateColumn;
                }

            }
        }
        public IRepository<Template> Template
        {
            get
            {
                if (_Template != null)
                {
                    return _Template;
                }
                else
                {
                    _Template = new Repository<Template>(_context);
                    return _Template;
                }

            }
        }
        public IRepository<PatientOrderMineral> PatientOrderMineral
        {
            get
            {
                if (_PatientOrderMineral != null)
                {
                    return _PatientOrderMineral;
                }
                else
                {
                    _PatientOrderMineral = new Repository<PatientOrderMineral>(_context);
                    return _PatientOrderMineral;
                }

            }
        }
        public IRepository<PatientOrderFitness> PatientOrderFitness
        {
            get
            {
                if (_PatientOrderFitness != null)
                {
                    return _PatientOrderFitness;
                }
                else
                {
                    _PatientOrderFitness = new Repository<PatientOrderFitness>(_context);
                    return _PatientOrderFitness;
                }

            }
        }
        public IRepository<PatientOrderDiet> PatientOrderDiet
        {
            get
            {
                if (_PatientOrderDiet != null)
                {
                    return _PatientOrderDiet;
                }
                else
                {
                    _PatientOrderDiet = new Repository<PatientOrderDiet>(_context);
                    return _PatientOrderDiet;
                }

            }
        }
        public IRepository<PatientOrderVitamin> PatientOrderVitamin
        {
            get
            {
                if (_PatientOrderVitamin != null)
                {
                    return _PatientOrderVitamin;
                }
                else
                {
                    _PatientOrderVitamin = new Repository<PatientOrderVitamin>(_context);
                    return _PatientOrderVitamin;
                }

            }
        }
        public IRepository<PatientOrderMetabolism> PatientOrderMetabolism
        {
            get
            {
                if (_PatientOrderMetabolism != null)
                {
                    return _PatientOrderMetabolism;
                }
                else
                {
                    _PatientOrderMetabolism = new Repository<PatientOrderMetabolism>(_context);
                    return _PatientOrderMetabolism;
                }

            }
        }
        public IRepository<PatientOrderEffectedGeneMetabolism> PatientOrderEffectedGeneMetabolism
        {
            get
            {
                if (_PatientOrderEffectedGeneMetabolism != null)
                {
                    return _PatientOrderEffectedGeneMetabolism;
                }
                else
                {
                    _PatientOrderEffectedGeneMetabolism = new Repository<PatientOrderEffectedGeneMetabolism>(_context);
                    return _PatientOrderEffectedGeneMetabolism;
                }

            }
        }
        public IRepository<PatientOrderEffectedGeneMineral> PatientOrderEffectedGeneMineral
        {
            get
            {
                if (_PatientOrderEffectedGeneMineral != null)
                {
                    return _PatientOrderEffectedGeneMineral;
                }
                else
                {
                    _PatientOrderEffectedGeneMineral = new Repository<PatientOrderEffectedGeneMineral>(_context);
                    return _PatientOrderEffectedGeneMineral;
                }

            }
        }
        public IRepository<PatientOrderEffectedGeneVitamin> PatientOrderEffectedGeneVitamin
        {
            get
            {
                if (_PatientOrderEffectedGeneVitamin != null)
                {
                    return _PatientOrderEffectedGeneVitamin;
                }
                else
                {
                    _PatientOrderEffectedGeneVitamin = new Repository<PatientOrderEffectedGeneVitamin>(_context);
                    return _PatientOrderEffectedGeneVitamin;
                }

            }
        }

        public IRepository<PatientOrderEffectedGeneDiet> PatientOrderEffectedGeneDiet
        {
            get
            {
                if (_PatientOrderEffectedGeneDiet != null)
                {
                    return _PatientOrderEffectedGeneDiet;
                }
                else
                {
                    _PatientOrderEffectedGeneDiet = new Repository<PatientOrderEffectedGeneDiet>(_context);
                    return _PatientOrderEffectedGeneDiet;
                }

            }
        }
        public IRepository<PatientOrderEffectedGeneFitness> PatientOrderEffectedGeneFitness
        {
            get
            {
                if (_PatientOrderEffectedGeneFitness != null)
                {
                    return _PatientOrderEffectedGeneFitness;
                }
                else
                {
                    _PatientOrderEffectedGeneFitness = new Repository<PatientOrderEffectedGeneFitness>(_context);
                    return _PatientOrderEffectedGeneFitness;
                }

            }
        }
        public IRepository<PatientOrderEpilepsy> PatientOrderEpilepsy
        {
            get
            {
                if (_PatientOrderEpilepsy != null)
                {
                    return _PatientOrderEpilepsy;
                }
                else
                {
                    _PatientOrderEpilepsy = new Repository<PatientOrderEpilepsy>(_context);
                    return _PatientOrderEpilepsy;
                }

            }
        }
        public IRepository<PatientOrderFatConsumption> PatientOrderFatConsumption
        {
            get
            {
                if (_PatientOrderFatConsumption != null)
                {
                    return _PatientOrderFatConsumption;
                }
                else
                {
                    _PatientOrderFatConsumption = new Repository<PatientOrderFatConsumption>(_context);
                    return _PatientOrderFatConsumption;
                }

            }
        }
        public IRepository<PatientOrderMethyation> PatientOrderMethyation
        {
            get
            {
                if (_PatientOrderMethyation != null)
                {
                    return _PatientOrderMethyation;
                }
                else
                {
                    _PatientOrderMethyation = new Repository<PatientOrderMethyation>(_context);
                    return _PatientOrderMethyation;
                }

            }
        }
        public IRepository<PatientOrderProstate> PatientOrderProstate
        {
            get
            {
                if (_PatientOrderProstate != null)
                {
                    return _PatientOrderProstate;
                }
                else
                {
                    _PatientOrderProstate = new Repository<PatientOrderProstate>(_context);
                    return _PatientOrderProstate;
                }

            }
        }













        public IRepository<PatientOrder> PatientOrder
        {
            get
            {
                if (_PatientOrder != null)
                {
                    return _PatientOrder;
                }
                else
                {
                    _PatientOrder = new Repository<PatientOrder>(_context);
                    return _PatientOrder;
                }

            }
        }
        public IRepository<ProstateTemplate> ProstateTemplate
        {
            get
            {
                if (_ProstateTemplate != null)
                {
                    return _ProstateTemplate;
                }
                else
                {
                    _ProstateTemplate = new Repository<ProstateTemplate>(_context);
                    return _ProstateTemplate;
                }

            }
        }

        public IRepository<EffectedGeneVitamin> EffectedGeneVitamin
        {
            get
            {
                if (_EffectedGeneVitamin != null)
                {
                    return _EffectedGeneVitamin;
                }
                else
                {
                    _EffectedGeneVitamin = new Repository<EffectedGeneVitamin>(_context);
                    return _EffectedGeneVitamin;
                }

            }
        }
        public IRepository<VitaminTemplate> VitaminTemplate
        {
            get
            {
                if (_VitaminTemplate != null)
                {
                    return _VitaminTemplate;
                }
                else
                {
                    _VitaminTemplate = new Repository<VitaminTemplate>(_context);
                    return _VitaminTemplate;
                }

            }
        }

        public IRepository<MineralTemplate> MineralTemplate
        {
            get
            {
                if (_MineralTemplate != null)
                {
                    return _MineralTemplate;
                }
                else
                {
                    _MineralTemplate = new Repository<MineralTemplate>(_context);
                    return _MineralTemplate;
                }

            }
        }
        public IRepository<DietTemplate> DietTemplate
        {
            get
            {
                if (_DietTemplate != null)
                {
                    return _DietTemplate;
                }
                else
                {
                    _DietTemplate = new Repository<DietTemplate>(_context);
                    return _DietTemplate;
                }

            }
        }
        public IRepository<EffectedGeneDiet> EffectedGeneDiet
        {
            get
            {
                if (_EffectedGeneDiet != null)
                {
                    return _EffectedGeneDiet;
                }
                else
                {
                    _EffectedGeneDiet = new Repository<EffectedGeneDiet>(_context);
                    return _EffectedGeneDiet;
                }

            }
        }
        public IRepository<MethyationTemplate> MethyationTemplate
        {
            get
            {
                if (_MethyationTemplate != null)
                {
                    return _MethyationTemplate;
                }
                else
                {
                    _MethyationTemplate = new Repository<MethyationTemplate>(_context);
                    return _MethyationTemplate;
                }

            }
        }
        public IRepository<EffectedGeneMineral> EffectedGeneMineral
        {
            get
            {
                if (_EffectedGeneMineral != null)
                {
                    return _EffectedGeneMineral;
                }
                else
                {
                    _EffectedGeneMineral = new Repository<EffectedGeneMineral>(_context);
                    return _EffectedGeneMineral;
                }

            }
        }

        public IRepository<FitnessTemplate> FitnessTemplate
        {
            get
            {
                if (_FitnessTemplate != null)
                {
                    return _FitnessTemplate;
                }
                else
                {
                    _FitnessTemplate = new Repository<FitnessTemplate>(_context);
                    return _FitnessTemplate;
                }

            }
        }
        public IRepository<FatConsumptionTemplate> FatConsumptionTemplate
        {
            get
            {
                if (_FatConsumptionTemplate != null)
                {
                    return _FatConsumptionTemplate;
                }
                else
                {
                    _FatConsumptionTemplate = new Repository<FatConsumptionTemplate>(_context);
                    return _FatConsumptionTemplate;
                }

            }
        }
        public IRepository<EffectedGeneFitness> EffectedGeneFitness
        {
            get
            {
                if (_EffectedGeneFitness != null)
                {
                    return _EffectedGeneFitness;
                }
                else
                {
                    _EffectedGeneFitness = new Repository<EffectedGeneFitness>(_context);
                    return _EffectedGeneFitness;
                }

            }
        }
        public IRepository<EpilepsyTemplate> EpilepsyTemplate
        {
            get
            {
                if (_EpilepsyTemplate != null)
                {
                    return _EpilepsyTemplate;
                }
                else
                {
                    _EpilepsyTemplate = new Repository<EpilepsyTemplate>(_context);
                    return _EpilepsyTemplate;
                }

            }
        }
        public IRepository<MetabolismTemplate> MetabolismTemplate
        {
            get
            {
                if (_MetabolismTemplate != null)
                {
                    return _MetabolismTemplate;
                }
                else
                {
                    _MetabolismTemplate = new Repository<MetabolismTemplate>(_context);
                    return _MetabolismTemplate;
                }

            }
        }
        public IRepository<EffectedGeneMetabolism> EffectedGeneMetabolism
        {
            get
            {
                if (_EffectedGeneMetabolism != null)
                {
                    return _EffectedGeneMetabolism;
                }
                else
                {
                    _EffectedGeneMetabolism = new Repository<EffectedGeneMetabolism>(_context);
                    return _EffectedGeneMetabolism;
                }

            }
        }
        public IRepository<EmailTrigger> EmailTrigger
        {
            get
            {
                if (_EmailTrigger != null)
                {
                    return _EmailTrigger;
                }
                else
                {
                    _EmailTrigger = new Repository<EmailTrigger>(_context);
                    return _EmailTrigger;
                }

            }
        }
        public IRepository<SMSConfig> SMSConfig
        {
            get
            {
                if (_SMSConfig != null)
                {
                    return _SMSConfig;
                }
                else
                {
                    _SMSConfig = new Repository<SMSConfig>(_context);
                    return _SMSConfig;
                }

            }
        }

        public IRepository<GroupClientList> GroupClientList
        {
            get
            {
                if (_GroupClientList != null)
                {
                    return _GroupClientList;
                }
                else
                {
                    _GroupClientList = new Repository<GroupClientList>(_context);
                    return _GroupClientList;
                }

            }
        }
        public IRepository<SMSGroup> SMSGroup
        {
            get
            {
                if (_SMSGroup != null)
                {
                    return _SMSGroup;
                }
                else
                {
                    _SMSGroup = new Repository<SMSGroup>(_context);
                    return _SMSGroup;
                }

            }
        }
        public IRepository<SMSTrigger> SMSTrigger
        {
            get
            {
                if (_SMSTrigger != null)
                {
                    return _SMSTrigger;
                }
                else
                {
                    _SMSTrigger = new Repository<SMSTrigger>(_context);
                    return _SMSTrigger;
                }

            }
        }
        public IRepository<SMS> SMS
        {
            get
            {
                if (_SMS != null)
                {
                    return _SMS;
                }
                else
                {
                    _SMS = new Repository<SMS>(_context);
                    return _SMS;
                }

            }
        }
        public IRepository<CustImage> CustImage
        {
            get
            {
                if (_CustImage != null)
                {
                    return _CustImage;
                }
                else
                {
                    _CustImage = new Repository<CustImage>(_context);
                    return _CustImage;
                }

            }
        }
        public IRepository<Email> Email { 
            get
            {
                if (_Email != null)
                {
                    return _Email;
                }
                else
                {
                    _Email = new Repository<Email>(_context);
                    return _Email;
                }

            }
        }
        public IRepository<CompanyProfile> CompanyProfile
        {
            get
            {
                if (_CompanyProfile != null)
                {
                    return _CompanyProfile;
                }
                else
                {
                    _CompanyProfile = new Repository<CompanyProfile>(_context);
                    return _CompanyProfile;
                }

            }
        }
        public IRepository<PatientPreAssesment> PatientPreAssesment
        {
            get
            {
                if (_PatientPreAssesment != null)
                {
                    return _PatientPreAssesment;
                }
                else
                {
                    _PatientPreAssesment = new Repository<PatientPreAssesment>(_context);
                    return _PatientPreAssesment;
                }

            }
        }
        public IRepository<PatientPreAssesmentVital> PatientPreAssesmentVital
        {
            get
            {
                if (_PatientPreAssesmentVital != null)
                {
                    return _PatientPreAssesmentVital;
                }
                else
                {
                    _PatientPreAssesmentVital = new Repository<PatientPreAssesmentVital>(_context);
                    return _PatientPreAssesmentVital;
                }

            }
        }
        public IRepository<PreAssessmentVitalDetail> PreAssessmentVitalDetail
        {
            get
            {
                if (_PreAssessmentVitalDetail != null)
                {
                    return _PreAssessmentVitalDetail;
                }
                else
                {
                    _PreAssessmentVitalDetail = new Repository<PreAssessmentVitalDetail>(_context);
                    return _PreAssessmentVitalDetail;
                }

            }
        }
        public IRepository<InitialMedicalAssessment> InitialMedicalAssessment
        {
            get
            {
                if (_InitialMedicalAssessment != null)
                {
                    return _InitialMedicalAssessment;
                }
                else
                {
                    _InitialMedicalAssessment = new Repository<InitialMedicalAssessment>(_context);
                    return _InitialMedicalAssessment;
                }

            }
        }
        public IRepository<InitialMedicalAssessmentDetail> InitialMedicalAssessmentDetail
        {
            get
            {
                if (_InitialMedicalAssessmentDetail != null)
                {
                    return _InitialMedicalAssessmentDetail;
                }
                else
                {
                    _InitialMedicalAssessmentDetail = new Repository<InitialMedicalAssessmentDetail>(_context);
                    return _InitialMedicalAssessmentDetail;
                }

            }
        }
        public IRepository<IMDetailLookup> IMDetailLookup
        {
            get
            {
                if (_IMDetailLookup != null)
                {
                    return _IMDetailLookup;
                }
                else
                {
                    _IMDetailLookup = new Repository<IMDetailLookup>(_context);
                    return _IMDetailLookup;
                }

            }
        }

        public IRepository<ViewControl> ViewControl
        {
            get
            {
                if (_viewControl != null)
                {
                    return _viewControl;
                }
                else
                {
                    _viewControl = new Repository<ViewControl>(_context);
                    return _viewControl;
                }

            }
        }

        public IRepository<PatientEncounter> PatientEncounter
        {
            get
            {
                if (_PatientEncounter != null)
                {
                    return _PatientEncounter;
                }
                else
                {
                    _PatientEncounter = new Repository<PatientEncounter>(_context);
                    return _PatientEncounter;
                }

            }
        }

        public IRepository<EducationLevel> EducationLevel
        {
            get
            {
                if (_viewControl != null)
                {
                    return _EducationLevel;
                }
                else
                {
                    _EducationLevel = new Repository<EducationLevel>(_context);
                    return _EducationLevel;
                }

            }
        }

        public IRepository<AccessPermission> AccessPermission
        {
            get
            {
                if (_accessPermission != null)
                {
                    return _accessPermission;
                }
                else
                {
                    _accessPermission = new Repository<AccessPermission>(_context);
                    return _accessPermission;
                }

            }
        }
        public IRepository<ActionPermission> ActionPermission
        {
            get
            {
                if (_accessPermission != null)
                {
                    return _actionPermission;
                }
                else
                {
                    _actionPermission = new Repository<ActionPermission>(_context);
                    return _actionPermission;
                }

            }
        }
        public IRepository<User> User
        {
            get
            {
                if (_user != null)
                {
                    return _user;
                }
                else
                {
                    _user = new Repository<User>(_context);
                    return _user;
                }

            }
        }
        public IRepository<Role> Role
        {
            get
            {
                if (_role != null)
                {
                    return _role;
                }
                else
                {
                    _role = new Repository<Role>(_context);
                    return _role;
                }

            }
        }
        public IRepository<ActionControl> ActionControl
        {
            get
            {
                if (_ActionControl != null)
                {
                    return _ActionControl;
                }
                else
                {
                    _ActionControl = new Repository<ActionControl>(_context);
                    return _ActionControl;
                }

            }
        }
        public IRepository<Appointment> Appointment
        {
            get
            {
                if (_Appointment != null)
                {
                    return _Appointment;
                }
                else
                {
                    _Appointment = new Repository<Appointment>(_context);
                    return _Appointment;
                }

            }
        }
        public IRepository<Attachment> Attachment
        {
            get
            {
                if (_Attachment != null)
                {
                    return _Attachment;
                }
                else
                {
                    _Attachment = new Repository<Attachment>(_context);
                    return _Attachment;
                }

            }
        }
        public IRepository<Bed> Bed
        {
            get
            {
                if (_Bed != null)
                {
                    return _Bed;
                }
                else
                {
                    _Bed = new Repository<Bed>(_context);
                    return _Bed;
                }

            }
        }
        public IRepository<BedAllocation> BedAllocation
        {
            get
            {
                if (_BedAllocation != null)
                {
                    return _BedAllocation;
                }
                else
                {
                    _BedAllocation = new Repository<BedAllocation>(_context);
                    return _BedAllocation;
                }

            }
        }
        public IRepository<Branch> Branch
        {
            get
            {
                if (_Branch != null)
                {
                    return _Branch;
                }
                else
                {
                    _Branch = new Repository<Branch>(_context);
                    return _Branch;
                }

            }
        }
        public IRepository<Building> Building
        {
            get
            {
                if (_Building != null)
                {
                    return _Building;
                }
                else
                {
                    _Building = new Repository<Building>(_context);
                    return _Building;
                }

            }
        }
        public IRepository<City> City
        {
            get
            {
                if (_City != null)
                {
                    return _City;
                }
                else
                {
                    _City = new Repository<City>(_context);
                    return _City;
                }

            }
        }
        public IRepository<Country> Country
        {
            get
            {
                if (_Country != null)
                {
                    return _Country;
                }
                else
                {
                    _Country = new Repository<Country>(_context);
                    return _Country;
                }

            }
        }
        public IRepository<Floor> Floor
        {
            get
            {
                if (_Floor != null)
                {
                    return _Floor;
                }
                else
                {
                    _Floor = new Repository<Floor>(_context);
                    return _Floor;
                }

            }
        }
        public IRepository<Lookup> Lookup
        {
            get
            {
                if (_Lookup != null)
                {
                    return _Lookup;
                }
                else
                {
                    _Lookup = new Repository<Lookup>(_context);
                    return _Lookup;
                }

            }
        }
        public IRepository<PatientAdmission> PatientAdmission
        {
            get
            {
                if (_PatientAdmission != null)
                {
                    return _PatientAdmission;
                }
                else
                {
                    _PatientAdmission = new Repository<PatientAdmission>(_context);
                    return _PatientAdmission;
                }

            }
        }
        public IRepository<PatientRelative> PatientRelative
        {
            get
            {
                if (_PatientRelative != null)
                {
                    return _PatientRelative;
                }
                else
                {
                    _PatientRelative = new Repository<PatientRelative>(_context);
                    return _PatientRelative;
                }

            }
        }
        public IRepository<PersonalInformation> PersonalInformation
        {
            get
            {
                if (_PersonalInformation != null)
                {
                    return _PersonalInformation;
                }
                else
                {
                    _PersonalInformation = new Repository<PersonalInformation>(_context);
                    return _PersonalInformation;
                }

            }
        }
        public IRepository<Room> Room
        {
            get
            {
                if (_Room != null)
                {
                    return _Room;
                }
                else
                {
                    _Room = new Repository<Room>(_context);
                    return _Room;
                }

            }
        }
        public IRepository<PatientPersonalInformation> PatientPersonalInformation
        {
            get
            {
                if (_PatientPersonalInformation != null)
                {
                    return _PatientPersonalInformation;
                }
                else
                {
                    _PatientPersonalInformation = new Repository<PatientPersonalInformation>(_context);
                    return _PatientPersonalInformation;
                }

            }
        }
        public IRepository<MedicalHistory> MedicalHistory
        {
            get
            {
                if (_MedicalHistory != null)
                {
                    return _MedicalHistory;
                }
                else
                {
                    _MedicalHistory = new Repository<MedicalHistory>(_context);
                    return _MedicalHistory;
                }

            }
        }
        public IRepository<LogTable> LogTable
        {
            get
            {
                if (_LogTable != null)
                {
                    return _LogTable;
                }
                else
                {
                    _LogTable = new Repository<LogTable>(_context);
                    return _LogTable;
                }

            }
        }
        public IRepository<LogData> LogData
        {
            get
            {
                if (_LogData != null)
                {
                    return _LogData;
                }
                else
                {
                    _LogData = new Repository<LogData>(_context);
                    return _LogData;
                }

            }
        }
    }
}
