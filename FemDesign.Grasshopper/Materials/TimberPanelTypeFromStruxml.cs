﻿// https://strusoft.com/
using System;
using System.Collections.Generic;
using Grasshopper.Kernel;

namespace FemDesign.Grasshopper
{
    public class TimberPanelTypeFromStruxml : FEM_Design_API_Component
    {
        public TimberPanelTypeFromStruxml() : base("TimberPlateLibrary.FromStruxml", "FromStruxml", "Load a custom MaterialDatabase which contains the TimberPanel type from a .struxml file.", CategoryName.Name(), SubCategoryName.Cat4a())
        {

        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("FilePath", "FilePath", "File path to .struxml file.", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("CltPanelType", "CltPanelType", "CltPanelLibraryType.", GH_ParamAccess.item);
            pManager.AddGenericParameter("OrthotropicShell", "OrthotropicShell", "OrthotropicPanelLibraryType", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string fillePath = null;
            DA.GetData(0, ref fillePath);

            var materialDatabase = Materials.MaterialDatabase.DeserializeStruxml(fillePath);
            List<Materials.CltPanelLibraryType> cltPaneltype = materialDatabase.GetCltPanelLibrary();
            List<Materials.OrthotropicPanelLibraryType> orthotropicPaneltype = materialDatabase.GetOrthotropicPanelLibrary();


            DA.SetDataList(0, cltPaneltype);
            DA.SetDataList(1, orthotropicPaneltype);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return FemDesign.Properties.Resources.MaterialTimberPlateMaterial;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("{4CD9372C-170D-4745-BE24-14DAAEB14CED}"); }
        }

        public override GH_Exposure Exposure => GH_Exposure.quarternary;

    }
}