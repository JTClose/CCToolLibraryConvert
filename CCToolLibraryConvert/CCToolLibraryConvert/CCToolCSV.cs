using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

using OrderedPropertyGrid;

namespace CCToolLibraryConvert
{
    [TypeConverter(typeof(PropertySorter))]
    public partial class CCToolCSV
    {
        public CCToolCSV()
        {
            _toolIndex = string.Empty;
            _vendor = string.Empty;
            _model = string.Empty;
            _url = string.Empty;
            _name = string.Empty;
            _type = string.Empty;
            _coating = string.Empty;
            _notes = string.Empty;
            _machine = string.Empty;
            _material = string.Empty;
            _cutpower = string.Empty;

            _number = 0;
            _diameter = 0;
            _cornerradius = 0;
            _flutelength = 0;
            _shaftdiameter = 0;
            _angle = 0;
            _numflutes = 0;
            _stickout = 0;
            _metric = 0;
            _plungerate = 0;
            _feedrate = 0;
            _rpm = 0;
            _depth = 0;
            _finishallowance = 0;
            _feedrate3d = 0;
            _rpm3d = 0;
            _stepover3d = 0;

        }

        #region Private

        private string _toolIndex;
        private int _number;
        private string _vendor;       // Limited optons ?
        private string _model;        // Descriptive ID
        private string _url;          // Shopping
        private string _name;         // Optional
        private string _type;         // Limited options  ball,end,vee,engraver
        private decimal _diameter;        // thousands inch
        private decimal _cornerradius;    // thousands inch
        private decimal _flutelength;     // thousands inch
        private decimal _shaftdiameter;    // ??
        private decimal _angle;            // Degrees
        private int _numflutes;           // Count
        private decimal _stickout;        // thousands inch  ?? What is this ?
        private string _coating;      // NULL
        private int _metric;               // 0 = Inch   1 = MM
        private string _notes;        // NULL
        private string _machine;      // NULL
        private string _material;     // NULL
        private decimal _plungerate;      // inch per minute
        private decimal _feedrate;        // inch per minute
        private int _rpm;                 // Rev pe minute
        private decimal _depth;           // thousands inch
        private string _cutpower;         // NULL ?
        private decimal _finishallowance;     // thousands inch
        private decimal _stepover3d;      // inch per minute 
        private decimal _feedrate3d;      // inch per minute
        private decimal _rpm3d;           // Rev per minute

        #endregion

        protected const string ID_CAT = "\t\t\t\tIdentification";
        protected const string TOOLDEF_CAT = "\t\t\tTool Definition";
        protected const string TOOLUSE_CAT = "\t\tTool Usage";
        protected const string CCPRO_CAT = "\tCCPro";
        protected const string ENVIRON_CAT = "Environment";

        #region Public Property

        [Browsable(false)]
        [ReadOnly(true)]
        [Description("Unique Integer ID")]
        [Category("Internal")]
        [DisplayName("ToolIndex")]
        public string ToolIndex              // Integer ID for CC
        {
            get
            {
                return this._toolIndex;
            }
            set
            {
                this._toolIndex = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Unique Integer ID")]
        [Category(ID_CAT), PropertyOrder(10)]
        [DisplayName("Number")]
        public int Number              // Integer ID for CC
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Tool OEM")]
        [Category(ID_CAT), PropertyOrder(20)]
        [DisplayName("Vendor")]
        public string Vendor       // Limited optons ?
        {
            get
            {
                return this._vendor;
            }
            set
            {
                this._vendor = value;
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Vendor Product ID")]
        [Category(ID_CAT), PropertyOrder(30)]
        [DisplayName("Model")]
        public string Model        // Descriptive ID
        {
            get
            {
                return this._model;
            }
            set
            {
                this._model = value;
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Web Link")]
        [Category(ID_CAT), PropertyOrder(50)]
        [DisplayName("URL")]
        public string URL          // Shopping
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Vendor Product ID")]
        [Category(ID_CAT), PropertyOrder(40)]
        [DisplayName("Name")]
        public string Name         // Optional
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Geometry Config")]
        [Category(TOOLDEF_CAT), PropertyOrder(10)]
        [DisplayName("Type")]
        public string Type         // Limited options  ball,end,vee,engraver
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Cutting Diameter")]
        [Category(TOOLDEF_CAT), PropertyOrder(20)]
        [DisplayName("Diameter")]
        public decimal Diameter        // thousands inch
        {
            get
            {
                return this._diameter;
            }
            set
            {
                this._diameter = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Edge Radius")]
        [Category(TOOLDEF_CAT), PropertyOrder(60)]
        [DisplayName("Corner Radius")]
        public decimal Cornerradius    // thousands inch
        {
            get
            {
                return this._cornerradius;
            }
            set
            {
                this._cornerradius = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Flute length")]
        [Category(TOOLDEF_CAT), PropertyOrder(50)]
        [DisplayName("Flute Length")]
        public decimal Flutelength     // thousands inch
        {
            get
            {
                return this._flutelength;
            }
            set
            {
                this._flutelength = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("*** IDK ***")]
        [Category(TOOLDEF_CAT), PropertyOrder(40)]
        [DisplayName("Shaft Diameter")]
        public decimal Shaftdiameter    // ??
        {
            get
            {
                return this._shaftdiameter;
            }
            set
            {
                this._shaftdiameter = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Tip Angle")]
        [Category(TOOLDEF_CAT), PropertyOrder(70)]
        [DisplayName("Angle")]
        public decimal Angle            // Degrees
        {
            get
            {
                return this._angle;
            }
            set
            {
                this._angle = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Number of Flutes")]
        [Category(TOOLDEF_CAT), PropertyOrder(30)]
        [DisplayName("Number of Flutes")]
        public int Numflutes           // Count
        {
            get
            {
                return this._numflutes;
            }
            set
            {
                this._numflutes = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("***** IDK *****")]
        [Category(TOOLDEF_CAT), PropertyOrder(80)]
        [DisplayName("Stick Out")]
        public decimal Stickout        // thousands inch  ?? What is this ?
        {
            get
            {
                return this._stickout;
            }
            set
            {
                this._stickout = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Surface Prep")]
        [Category(TOOLDEF_CAT),PropertyOrder(90)]
        [DisplayName("Coating")]
        public string Coating      // NULL
        {
            get
            {
                return this._coating;
            }
            set
            {
                this._coating = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Unit of Measure")]
        [Category(ENVIRON_CAT), PropertyOrder(10)]
        [DisplayName("Units")]
        public int Metric              // 0 = Inch   1 = MM
        {
            get
            {
                return this._metric;
            }
            set
            {
                this._metric = value;
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Comment")]
        [Category(ENVIRON_CAT), PropertyOrder(40)]
        [DisplayName("Note")]
        public string Notes        // NULL
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes = value;
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Execute")]
        [Category(ENVIRON_CAT), PropertyOrder(30)]
        [DisplayName("Machine")]
        public string Machine      // NULL
        {
            get
            {
                return this._machine;
            }
            set
            {
                this._machine = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Stock Material")]
        [Category(ENVIRON_CAT), PropertyOrder(20)]
        [DisplayName("Material")]
        public string Material     // NULL
        {
            get
            {
                return this._material;
            }
            set
            {
                this._material = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Rate of Plunge")]
        [Category(TOOLUSE_CAT), PropertyOrder(40)]
        [DisplayName("Plunge Rate")]
        public decimal Plungerate      // inch per minute
        {
            get
            {
                return this._plungerate;
            }
            set
            {
                this._plungerate = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Rate of Feed")]
        [Category(TOOLUSE_CAT), PropertyOrder(30)]
        [DisplayName("Feed Rate")]
        public decimal Feedrate        // inch per minute  Units?
        {
            get
            {
                return this._feedrate;
            }
            set
            {
                this._feedrate = value;
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Revolution Per Min")]
        [Category(TOOLUSE_CAT)]
        [DisplayName("RPM"), PropertyOrder(20)]
        public int Rpm                 // Rev pe minute
        {
            get
            {
                return this._rpm;
            }
            set
            {
                this._rpm = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Depth of *** IDK ***")]
        [Category(TOOLUSE_CAT), PropertyOrder(10)]
        [DisplayName("Depth")]
        public decimal Depth           // thousands inch
        {
            get
            {
                return this._depth;
            }
            set
            {
                this._depth = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("CCPro Cut Power")]
        [Category(CCPRO_CAT), PropertyOrder(50)]
        [DisplayName("Cut Power")]
        public string Cutpower         // NULL ?
        {
            get
            {
                return this._cutpower;
            }
            set
            {
                this._cutpower = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("CCPro Finish Allowance")]
        [Category(CCPRO_CAT), PropertyOrder(40)]
        [DisplayName("Finish Allowance")]
        public decimal Finishallowance     // thousands inch
        {
            get
            {
                return this._finishallowance;
            }
            set
            {
                this._finishallowance = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("CCPro 3D StepOver")]
        [Category(CCPRO_CAT), PropertyOrder(30)]
        [DisplayName("3D StepOver")]
        public decimal Stepover3d      // inch per minute 
        {
            get
            {
                return this._stepover3d;
            }
            set
            {
                this._stepover3d = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("CCPro 3D FeedRate")]
        [Category(CCPRO_CAT), PropertyOrder(20)]
        [DisplayName("3D Feed Rate")]
        public decimal Feedrate3d      // inch per minute
        {
            get
            {
                return this._feedrate3d;
            }
            set
            {
                this._feedrate3d = value;
            }
        }


        [Browsable(true)]
        [ReadOnly(false)]
        [Description("CCPro 3D rpm")]
        [Category(CCPRO_CAT), PropertyOrder(10)]
        [DisplayName("3D RPM")]
        public decimal Rpm3d           // Rev per minute
        {
            get
            {
                return this._rpm3d;
            }
            set
            {
                this._rpm3d = value;
            }
        }
        #endregion

        #region Method
        public CCToolCSV ShallowCopy()
        {
            return (CCToolCSV)this.MemberwiseClone();

        }
        #endregion


    }
}
