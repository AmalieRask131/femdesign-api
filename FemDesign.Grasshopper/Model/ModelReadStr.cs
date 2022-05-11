// https://strusoft.com/
using System;
using System.Linq;
using System.Collections.Generic;
using Grasshopper.Kernel;

namespace FemDesign.Grasshopper
{
    public class ModelReadStr: GH_Component
    {
        public ModelReadStr(): base("Model.ReadStr", "ReadStr", "Read model from .str file.", "FEM-Design", "Model")
        {

        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("StrPath", "StrPath", "File path to FEM-Design model (.str) file.", GH_ParamAccess.item);
            pManager.AddTextParameter("FilePathBsc", "FilePathBsc", "File path to .bsc batch-file. Item or list.", GH_ParamAccess.list);
            pManager[pManager.ParamCount - 1].Optional = true;
            pManager.AddTextParameter("ResultTypes", "ResultTypes", "Results to be extracted from model. This might require the model to have been analysed. Item or list.", GH_ParamAccess.list);
            pManager[pManager.ParamCount - 1].Optional = true;
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("FdModel", "FdModel", "FdModel.", GH_ParamAccess.item);
            pManager.Register_GenericParam("FdFeaModel", "FdFeaModel", "FdFeaModel.");
            pManager.AddGenericParameter("Results", "Results", "Results.", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Get input
            string filePath = null;
            List<string> bscPaths = new List<string>();
            List<string> resultTypes = new List<string>();
            Results.FDfea fdFeaModel = null;

            DA.GetData("StrPath", ref filePath);
            DA.GetDataList("FilePathBsc", bscPaths);
            DA.GetDataList("ResultTypes", resultTypes);
            if (filePath == null)
            {
                return;
            }


            // It needs to check if model has been runned
            // Always Return the FeaNode Result
            resultTypes.Insert(0, "FeaNode");
            resultTypes.Insert(1, "FeaShell");


            var _resultTypes = resultTypes.Select(r => GenericClasses.EnumParser.Parse<Results.ResultType>(r));


            // Create Bsc files from resultTypes
            var listProcs = _resultTypes.Select(r => Results.ResultAttributeExtentions.ListProcs[r]);
            

            var dir = System.IO.Path.GetDirectoryName(filePath);
            var batchResults = listProcs.SelectMany(lp => lp.Select(l => new Calculate.Bsc(l, $"{dir}\\{l}.bsc")));
            var bscPathsFromResultTypes = batchResults.Select(bsc => bsc.BscPath).ToList();

            // Create FdScript
            var allBscPaths = bscPaths.Concat(bscPathsFromResultTypes).ToList();
            var fdScript = FemDesign.Calculate.FdScript.ReadStr(filePath, allBscPaths);

            // Run FdScript
            var app = new FemDesign.Calculate.Application();
            bool hasExited = app.RunFdScript(fdScript, false, true, false);

            // Read model and results
            var model = Model.DeserializeFromFilePath(fdScript.StruxmlPath);

            IEnumerable<Results.IResult> results = Enumerable.Empty<Results.IResult>();

            List<Results.FeaNode> feaNodeRes = new List<Results.FeaNode>();
            List<Results.FeaShell> feaShellRes = new List<Results.FeaShell>();

            if (resultTypes != null && resultTypes.Any())
            {
                foreach (var cmd in fdScript.CmdListGen)
                {
                    string path = cmd.OutFile;
                    try
                    {
                        if(path.Contains("FeaNode"))
                        {
                            feaNodeRes = Results.ResultsReader.Parse(path).Cast<Results.FeaNode>().ToList();
                        }
                        else if (path.Contains("FeaShell"))
                        {
                            feaShellRes = Results.ResultsReader.Parse(path).Cast<Results.FeaShell>().ToList();
                        }
                        else
                        {
                            var _results = Results.ResultsReader.Parse(path);
                            results = results.Concat(_results);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.InnerException.Message);
                    }
                }
            }

            fdFeaModel = new FemDesign.Results.FDfea(feaNodeRes, feaShellRes);

            // Set output
            DA.SetData("FdModel", model);
            DA.SetData("FdFeaModel", fdFeaModel);
            DA.SetDataList("Results", results);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return FemDesign.Properties.Resources.ModelReadStr;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("e5d933c4-9217-4ffa-9f82-15a5a26c9967"); }
        }
    } 
}