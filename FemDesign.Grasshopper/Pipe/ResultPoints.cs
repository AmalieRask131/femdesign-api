﻿//// https://strusoft.com/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Grasshopper.Kernel;
//using Rhino.Geometry;

//namespace FemDesign.Grasshopper
//{
//    public class ResultPoints : GH_Component
//    {
//        public ResultPoints() : base("ResultPoints", "ResultPoints", "Return Result in a specific point", CategoryName.Name(), SubCategoryName.Cat7b())
//        {

//        }
//        protected override void RegisterInputParams(GH_InputParamManager pManager)
//        {
//            pManager.AddPointParameter("Point", "Point", "", GH_ParamAccess.item);
//            pManager.AddGenericParameter("StructuralElement", "StructuralElement", "", GH_ParamAccess.item);
//            pManager.AddTextParameter("Identifier", "Identifier", "Identifier. Optional, default value if undefined.", GH_ParamAccess.item, "LS");
//            pManager[pManager.ParamCount - 1].Optional = true;
//        }
//        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
//        {
//            pManager.AddGenericParameter("ResultPoint", "ResultPoint", "", GH_ParamAccess.item);
//        }
//        protected override void SolveInstance(IGH_DataAccess DA)
//        {
//            // get input
//            Rhino.Geometry.Point3d point = Rhino.Geometry.Point3d.Origin;
//            DA.GetData(0, ref point);

//            FemDesign.GenericClasses.IStructureElement structuralElement = null;
//            DA.GetData(1, ref structuralElement);

//            string identifier = "PT";
//            DA.GetData(2, ref identifier);

//            var resPoint = new FemDesign.Calculate.CmdResultPoint(point.FromRhino(), structuralElement, identifier);

//            // output
//            DA.SetData(0, resPoint);
//        }
//        protected override System.Drawing.Bitmap Icon
//        {
//            get
//            {
//                return FemDesign.Properties.Resources.ResultPoint;
//            }
//        }
//        public override Guid ComponentGuid
//        {
//            get { return new Guid("{810A6AC6-2453-489A-96E1-5129A1F7DCD3}"); }
//        }
//        public override GH_Exposure Exposure => GH_Exposure.hidden;
//    }
//}