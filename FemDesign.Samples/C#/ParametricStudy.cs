﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FemDesign.Samples
{
    public partial class SampleProgram
    {
        private static void ParametricStudy()
        {
            // Set the different paths and folders relevant to the example
            string struxmlPath = "ExampleModels/sample_slab.struxml";
            string outFolder = "ExampleModels/output/";
            string bscPath = Path.GetFullPath("ExampleModels/pointsupportreactions.bsc");
            List<string> bscPaths = new List<string>();
            bscPaths.Add(bscPath);

            // Create the output directory if it does not already exists
            if (!Directory.Exists(outFolder))
                Directory.CreateDirectory(outFolder);

            // Read original struxml model
            Model model = Model.DeserializeFromFilePath(struxmlPath);

            // Read slab number 5 and its material (hard coded in this example, probably better to look for a slab with a certain name, eg. P.1)
            Shells.Slab slab = model.Entities.Slabs[4];
            Materials.Material material = model.Entities.Slabs[4].Material;
            double Ecm = Convert.ToDouble(material.Concrete.Ecm);

            // Iterate over model using different E-modulus for the slab
            for (int i = 1; i < 6; i++)
            {
                // Change E-modulus
                double new_Ecm = Math.Round(0.2 * i * Ecm);
                material.Concrete.Ecm = Convert.ToString(new_Ecm);

                // Save struxml
                string outPathIndividual = Path.GetFullPath(outFolder + "sample_slab_out" + Convert.ToString(new_Ecm) + ".struxml");
                model.SerializeModel(outPathIndividual);

                // Run analysis
                Calculate.Analysis analysis = new Calculate.Analysis(null, null, null, null, calcCase: true, false, false, false, false, false, false, false, false, false, false, false, false);
                FemDesign.Calculate.FdScript fdScript = FemDesign.Calculate.FdScript.Analysis(outPathIndividual, analysis, bscPaths, "", true);
                Calculate.Application app = new Calculate.Application();
                app.RunFdScript(fdScript, false, true, true);

                string pointSupportReactionsPath = Path.Combine(outFolder, "pointsupportreactions.csv");

                // One way of reading results, available for some result types only yet
                
                var results = Results.ResultsReader.Parse(pointSupportReactionsPath);
                var pointSupportReactions = results.Cast<Results.PointSupportReaction>().ToList();

                Console.WriteLine();
                Console.WriteLine($"Emean: {new_Ecm}");
                Console.WriteLine("Id         | Reaction  ");
                foreach (var reaction in pointSupportReactions)
                {
                    Console.WriteLine($"{reaction.Id,10} | {reaction.Fz,10}");
                }
            }
            Console.ReadKey();
        }
    }
}
