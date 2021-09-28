using Repository.UnitOfWork;
using Service.Services;

namespace Service.UnitOfServices
{
    public class UnitOfService : IUnitOfService
    {
        private IUnitOfWork _rep;
        private ViewControlService _ViewControlService;
        private AccessPermissionService _AccessPermissionService;
        private ActionPermissionService _ActionPermissionService;
        private UserService _UserService;
        private RoleService _RoleService;
        private BedAllocationService _BedAllocationService;
        private BedService _BedService;
        private BranchService _BranchService;
        private BuildingService _BuildingService;
        private CityService _CityService;
        private CountryService _CountryService;
        private FloorService _FloorService;
        private LookupService _LookupService;
        private PatientService _PatientService;
        private PersonalInformationService _PersonalInformationService;
        private RoomService _RoomService;
        private AppointmentService _AppointmentService;
        private ActionControlService _ActionControlService;
        private PatientPersonalInformationService _PatientPersonalInformationService;
        private EducationLevelService _EducationLevelService;
        private LogTableService _LogTableService;
        private AttachmentService _AttachmentService;
        private PatientEncounterService _PatientEncounterService;
        private CompanyProfileService _CompanyProfileService;
        private EmailService _EmailService;
        private SMSService _SMSService;
        private TemplateService _TemplateService;
        private PatientOrderService _PatientOrderService;
        private DynamicTemplateService _DynamicTemplateService;
        private ClientOrderService _ClientOrderService;
        private MedicalDictionaryService _MedicalDictionaryService;
        private TestDynamicTemplateService _TestDynamicTemplateService;
        private TestTempService _TestTempService;
        private ReminderService _ReminderService;
        private FollowUpService _FollowUpService;
        private AppointmentExpService _AppointmentExpService;

        public UnitOfService()
        {
            this._rep = new UnitOfWork();
        }
        public AppointmentExpService AppointmentExpService
        {
            get
            {
                if (_AppointmentExpService != null)
                    return _AppointmentExpService;
                else
                {
                    _AppointmentExpService = new AppointmentExpService(_rep);
                    return _AppointmentExpService;
                }
            }
        }
        public FollowUpService FollowUpService
        {
            get
            {
                if (_FollowUpService != null)
                    return _FollowUpService;
                else
                {
                    _FollowUpService = new FollowUpService(_rep);
                    return _FollowUpService;
                }
            }
        }
        public ReminderService ReminderService
        {
            get
            {
                if (_ReminderService != null)
                    return _ReminderService;
                else
                {
                    _ReminderService = new ReminderService(_rep);
                    return _ReminderService;
                }
            }
        }
        public TestTempService TestTempService
        {
            get
            {
                if (_TestTempService != null)
                    return _TestTempService;
                else
                {
                    _TestTempService = new TestTempService(_rep);
                    return _TestTempService;
                }
            }
        }

        public TestDynamicTemplateService TestDynamicTemplateService
        {
            get
            {
                if (_TestDynamicTemplateService != null)
                {
                    return _TestDynamicTemplateService;
                }
                else
                {
                    _TestDynamicTemplateService = new TestDynamicTemplateService(_rep);
                    return _TestDynamicTemplateService;
                }
            }
        }

        public MedicalDictionaryService MedicalDictionaryService
        {
            get
            {
                if (_MedicalDictionaryService != null)
                {
                    return _MedicalDictionaryService;
                }
                else
                {
                    _MedicalDictionaryService = new MedicalDictionaryService(_rep);
                    return _MedicalDictionaryService;
                }
            }
        }
        public ClientOrderService ClientOrderService
        {
            get
            {
                if (_ClientOrderService != null)
                {
                    return _ClientOrderService;
                }
                else
                {
                    _ClientOrderService = new ClientOrderService(_rep);
                    return _ClientOrderService;
                }
            }
        }
        public DynamicTemplateService DynamicTemplateService
        {
            get
            {
                if (_DynamicTemplateService != null)
                {
                    return _DynamicTemplateService;
                }
                else
                {
                    _DynamicTemplateService = new DynamicTemplateService(_rep);
                    return _DynamicTemplateService;
                }
            }
        }
        public TemplateService TemplateService
        {
            get
            {
                if (_TemplateService != null)
                {
                    return _TemplateService;
                }
                else
                {
                    _TemplateService = new TemplateService(_rep);
                    return _TemplateService;
                }
            }
        }
        public PatientOrderService PatientOrderService
        {
            get
            {
                if (_PatientOrderService != null)
                {
                    return _PatientOrderService;
                }
                else
                {
                    _PatientOrderService = new PatientOrderService(_rep);
                    return _PatientOrderService;
                }
            }
        }

        public CompanyProfileService CompanyProfileService
        {
            get
            {
                if (_CompanyProfileService != null)
                {
                    return _CompanyProfileService;
                }
                else
                {
                    _CompanyProfileService = new CompanyProfileService(_rep);
                    return _CompanyProfileService;
                }
            }
        }
        public SMSService SMSService
        {
            get
            {
                if (_SMSService != null)
                {
                    return _SMSService;
                }
                else
                {
                    _SMSService = new SMSService(_rep);
                    return _SMSService;
                }
            }
        }
        public EmailService EmailService
        {
            get
            {
                if (_EmailService != null)
                {
                    return _EmailService;
                }
                else
                {
                    _EmailService = new EmailService(_rep);
                    return _EmailService;
                }
            }
        }
        public PatientEncounterService PatientEncounterService
        {
            get
            {
                if (_PatientEncounterService != null)
                {
                    return _PatientEncounterService;
                }
                else
                {
                    _PatientEncounterService = new PatientEncounterService(_rep);
                    return _PatientEncounterService;
                }
            }
        }
        public BedAllocationService BedAllocationService
        {
            get
            {
                if (_BedAllocationService != null)
                {
                    return _BedAllocationService;
                }
                else
                {
                    _BedAllocationService = new BedAllocationService(_rep);
                    return _BedAllocationService;
                }
            }
        }

        public EducationLevelService EducationLevelService
        {
            get
            {
                if (_EducationLevelService != null)
                {
                    return _EducationLevelService;
                }
                else
                {
                    _EducationLevelService = new EducationLevelService(_rep);
                    return _EducationLevelService;
                }
            }
        }
        public BedService BedService
        {
            get
            {
                if (_BedService != null)
                {
                    return _BedService;
                }
                else
                {
                    _BedService = new BedService(_rep);
                    return _BedService;
                }
            }
        }
        public BranchService BranchService
        {
            get
            {
                if (_BranchService != null)
                {
                    return _BranchService;
                }
                else
                {
                    _BranchService = new BranchService(_rep);
                    return _BranchService;
                }
            }
        }
        public BuildingService BuildingService
        {
            get
            {
                if (_BuildingService != null)
                {
                    return _BuildingService;
                }
                else
                {
                    _BuildingService = new BuildingService(_rep);
                    return _BuildingService;
                }
            }
        }
        public CityService CityService
        {
            get
            {
                if (_CityService != null)
                {
                    return _CityService;
                }
                else
                {
                    _CityService = new CityService(_rep);
                    return _CityService;
                }
            }
        }
        public CountryService CountryService
        {
            get
            {
                if (_CountryService != null)
                {
                    return _CountryService;
                }
                else
                {
                    _CountryService = new CountryService(_rep);
                    return _CountryService;
                }
            }
        }
        public FloorService FloorService
        {
            get
            {
                if (_FloorService != null)
                {
                    return _FloorService;
                }
                else
                {
                    _FloorService = new FloorService(_rep);
                    return _FloorService;
                }
            }
        }
        public LookupService LookupService
        {
            get
            {
                if (_LookupService != null)
                {
                    return _LookupService;
                }
                else
                {
                    _LookupService = new LookupService(_rep);
                    return _LookupService;
                }
            }
        }
        public PatientService PatientService
        {
            get
            {
                if (_PatientService != null)
                {
                    return _PatientService;
                }
                else
                {
                    _PatientService = new PatientService(_rep);
                    return _PatientService;
                }
            }
        }
        public PersonalInformationService PersonalInformationService
        {
            get
            {
                if (_PersonalInformationService != null)
                {
                    return _PersonalInformationService;
                }
                else
                {
                    _PersonalInformationService = new PersonalInformationService(_rep);
                    return _PersonalInformationService;
                }
            }
        }
        public RoomService RoomService
        {
            get
            {
                if (_RoomService != null)
                {
                    return _RoomService;
                }
                else
                {
                    _RoomService = new RoomService(_rep);
                    return _RoomService;
                }
            }
        }
        public ViewControlService ViewControlService
        {
            get
            {
                if (_ViewControlService != null)
                {
                    return _ViewControlService;
                }
                else
                {
                    _ViewControlService = new ViewControlService(_rep);
                    return _ViewControlService;
                }

            }
        }
        public AccessPermissionService AccessPermissionService
        {
            get
            {
                if (_AccessPermissionService != null)
                {
                    return _AccessPermissionService;
                }
                else
                {
                    _AccessPermissionService = new AccessPermissionService(_rep);
                    return _AccessPermissionService;
                }

            }
        }
        public ActionPermissionService ActionPermissionService
        {
            get
            {
                if (_ActionPermissionService != null)
                {
                    return _ActionPermissionService;
                }
                else
                {
                    _ActionPermissionService = new ActionPermissionService(_rep);
                    return _ActionPermissionService;
                }
            }
        }
        public UserService UserService
        {
            get
            {
                if (_UserService != null)
                {
                    return _UserService;
                }
                else
                {
                    _UserService = new UserService(_rep);
                    return _UserService;
                }
            }
        }
        public RoleService RoleService
        {
            get
            {
                if (_RoleService != null)
                {
                    return _RoleService;
                }
                else
                {
                    _RoleService = new RoleService(_rep);
                    return _RoleService;
                }
            }
        }
        public AppointmentService AppointmentService
        {
            get
            {
                if (_AppointmentService != null)
                {
                    return _AppointmentService;
                }
                else
                {
                    _AppointmentService = new AppointmentService(_rep);
                    return _AppointmentService;
                }
            }
        }
        public ActionControlService ActionControlService
        {
            get
            {
                if (_ActionControlService != null)
                {
                    return _ActionControlService;
                }
                else
                {
                    _ActionControlService = new ActionControlService(_rep);
                    return _ActionControlService;
                }
            }
        }
        public PatientPersonalInformationService PatientPersonalInformationService
        {
            get
            {
                if (_PatientPersonalInformationService != null)
                {
                    return _PatientPersonalInformationService;
                }
                else
                {
                    _PatientPersonalInformationService = new PatientPersonalInformationService(_rep);
                    return _PatientPersonalInformationService;
                }
            }
        }
        public LogTableService LogTableService
        {
            get
            {
                if (_LogTableService != null)
                {
                    return _LogTableService;
                }
                else
                {
                    _LogTableService = new LogTableService(_rep);
                    return _LogTableService;
                }
            }
        }
        public AttachmentService AttachmentService
        {
            get
            {
                if (_AttachmentService != null)
                {
                    return _AttachmentService;
                }
                else
                {
                    _AttachmentService = new AttachmentService(_rep);
                    return _AttachmentService;
                }
            }
        }

    }
}
