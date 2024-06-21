using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics.CodeAnalysis;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.DB.Plumbing;
using System.Windows.Shapes;
using Line = Autodesk.Revit.DB.Line;
using Autodesk.Revit.DB.Structure.StructuralSections;
using System.Security.Cryptography;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Mechanical;
using System.Windows.Documents;
using System.Windows.Controls;
using Grid = Autodesk.Revit.DB.Grid;
using System.Security.AccessControl;

namespace generalsolution
{
    class Filter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Wall)
            {
                if (elem.Document.GetElement(elem.Id).Name == "Generic - 200mm")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }

    //filter_for mechducts
    class linesfilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is CurveElement)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
    class textfilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is TextNote)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }


    class instancefilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is FamilyInstance)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
    class gridfilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Grid)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
    class levelfilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Level)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }

    class rebarfilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Rebar)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
    class LinkCadFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is ImportInstance)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }

    class floorfilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
           if (elem is Floor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class revitplugin : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region generalmethod
            UIApplication uiapp = commandData.Application;
            Application app = commandData.Application.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            method m = new method(doc);

            #endregion
            TransactionGroup tg = new TransactionGroup(doc, "tg");
            tg.Start();
            #region col_names
            //IList<Element> element_type = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).ToElements().Where(c => c.Category.Name == "Structural Columns").ToList();

            //Transaction T1 = new Transaction(doc, "t1");
            //{

            //    T1.Start();

            //    foreach (Element et in element_type)
            //    {
            //        try
            //        {
            //            string width = (et.ParametersMap.get_Item("width").AsDouble() * 304.8 / 10).ToString();
            //            string length = (et.ParametersMap.get_Item("length").AsDouble() * 304.8 / 10).ToString();

            //            et.Name = et.Name + "(" + width + "*" + length + ")";

            //        }
            //        catch
            //        {
            //            et.Name = et.Name + "non structural";


            //    }

            //    T1.Commit();

            //}
            #endregion
            #region
            ////collect curtain walls
            //List<Wall> curtainwalls = new FilteredElementCollector(doc).OfClass(typeof(Wall)).ToElements().Where(e => (e as Wall).CurtainGrid == null).ToList().Select(r => r as Wall).ToList();
            ////collect view family types
            //#region curtainwall_archplugin
            //Element tt = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).ToElements().Where(v => (v as ViewFamilyType).ViewFamily == ViewFamily.ThreeDimensional).ToList().First();

            //m.Showdialogbox(curtainwalls.Select(w => w.Name));
            //View3D newview = null;
            //Transaction t2 = new Transaction(doc, "create_view");
            //t2.Start();
            ////create 3dview
            //newview = View3D.CreateIsometric(doc, tt.Id);
            //newview.Name = "fathy";
            //newview.IsolateElementsTemporary(curtainwalls.Select(c => c.Id).ToList());
            //t2.Commit();
            //uidoc.ActiveView = newview;
            #endregion
            
   
            #region check rebar length  example
            List<Element> reb = new FilteredElementCollector(doc).OfClass(typeof(Rebar)).ToElements().Where(wr => (wr as Rebar).TotalLength <= 6630 / 304.8).ToList();
            Transaction t_color = new Transaction(doc, "t_col");
            {
                t_color.Start();
                foreach (Element el in reb)
                {
                    OverrideGraphicSettings settings = uidoc.ActiveView.GetElementOverrides(el.Id);
                    settings = settings.SetProjectionLineColor(new Color(250, 0, 0));
                    uidoc.ActiveView.SetElementOverrides(el.Id, settings);
                }
                uidoc.ActiveView.DisplayStyle = DisplayStyle.HLR;
                t_color.Commit();
            }
            #endregion
            



            tg.Assimilate();
            return Result.Succeeded;

        }
       
        }
    }

      

