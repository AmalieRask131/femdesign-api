// https://strusoft.com/
using System.Collections.Generic;

#region dynamo
using Autodesk.DesignScript.Runtime;

namespace FemDesign
{
    /// <summary>
    /// Static methods from other classes are put under this class Dynamo library heirarchy reasons 
    /// so that all deconstruc methods are organized under Deconstruct.
    /// </summary>
    [IsVisibleInDynamoLibrary(false)]
    public class Deconstruct
    {
        /// <summary>
        /// Deconstruct an axis element
        /// </summary>
        /// <param name="axis">Axis.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Line", "Prefix", "Id", "IdIsLetter"})]
        public static Dictionary<string, object> AxisDeconstruct(FemDesign.StructureGrid.Axis axis)
        {
            return new Dictionary<string, object>
            {
                {"Guid", axis.Guid},
                {"Line", Autodesk.DesignScript.Geometry.Line.ByStartPointEndPoint(axis.StartPoint.ToDynamo(), axis.EndPoint.ToDynamo())},
                {"Prefix", axis.Prefix},
                {"Id", axis.Id},
                {"IdIsLetter", axis.IdIsLetter}
            };
        }
        /// <summary>
        /// Deconstruct a bar element.
        /// </summary>
        /// <param name="bar">Bar.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "StructuralID", "AnalyticalID", "Type", "Curve", "Material", "Section"})]
        public static Dictionary<string, object> BarDeconstruct(FemDesign.Bars.Bar bar)
        {
            return new Dictionary<string, object>
            {
                {"Guid", bar.Guid},
                {"AnalyticalID", bar.Identifier},
                {"StructuralID", bar.BarPart.Identifier},
                {"Type", bar.Type},
                {"Curve", bar.GetDynamoCurve()},
                {"Material", bar.BarPart.Material},
                {"Section", bar.BarPart.Sections}
            };
        }

        /// <summary>
        /// Deconstruct a fictitious bar element.
        /// </summary>
        /// <param name="fictitiousBar">FictitiousBar.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "AnalyticalID", "Curve", "AE", "ItG", "I1E", "I2E", "Connectivity", "LocalY"})]
        public static Dictionary<string, object> FictitiousBarDeconstruct(FemDesign.ModellingTools.FictitiousBar fictitiousBar)
        {
            return new Dictionary<string, object>
            {
                {"Guid", fictitiousBar.Guid},
                {"AnalyticalID", fictitiousBar.Name},
                {"Curve", fictitiousBar.Edge.ToDynamo()},
                {"AE", fictitiousBar.AE},
                {"ItG", fictitiousBar.ItG},
                {"I1E", fictitiousBar.I1E},
                {"I2E", fictitiousBar.I2E},
                {"Connectivity", fictitiousBar._connectivity},
                {"LocalY", fictitiousBar.LocalY.ToDynamo()}
            };
        }

        /// <summary>
        /// Deconstruct a cover.
        /// </summary>
        /// <param name="cover">Cover.</param>
        /// <returns></returns>
        [MultiReturn(new[]{"Guid", "Id", "Surface", "Contours"})]
        [IsVisibleInDynamoLibrary(true)]

        public static Dictionary<string, object> CoverDeconstruct(FemDesign.Cover cover)
        {
            return new Dictionary<string, object>
            {
                {"Guid", cover.Guid},
                {"Id", cover.Identifier},
                {"Surface", cover.GetDynamoSurface()},
                {"Contours", cover.GetDynamoCurves()}
            };
        }

        /// <summary>
        /// Deconstruct a PointLoad.
        /// </summary>
        /// <param name="pointLoad">PointLoad.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Type", "Point", "Direction", "q", "LoadCaseGuid", "Comment"})]
        public static Dictionary<string, object> PointLoadDeconstruct(FemDesign.Loads.PointLoad pointLoad)
        {
            return new Dictionary<string, object>
            {
                {"Guid", pointLoad.Guid},
                {"Type", pointLoad.LoadType},
                {"Point", pointLoad.GetDynamoGeometry()},
                {"Direction", pointLoad.Direction.ToDynamo()},
                {"q", pointLoad.Load.Value},
                {"LoadCaseGuid", pointLoad.LoadCase},
                {"Comment", pointLoad.Comment}
            };
        }

        /// <summary>
        /// Deconstruct a LineLoad.
        /// </summary>
        /// <param name="lineLoad">LineLoad.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Type", "Curve", "Direction", "q1", "q2", "LoadCaseGuid", "Comment"})]
        public static Dictionary<string, object> LineLoadDeconstruct(FemDesign.Loads.LineLoad lineLoad)
        {
            return new Dictionary<string, object>
            {
                {"Guid", lineLoad.Guid},
                {"Type", lineLoad.LoadType},
                {"Curve", lineLoad.GetDynamoGeometry()},
                {"Direction", lineLoad.Direction.ToDynamo()},
                {"q1", lineLoad.Load[0].Value},
                {"q2", lineLoad.Load[1].Value},
                {"LoadCaseGuid", lineLoad.LoadCase},
                {"Comment", lineLoad.Comment}
            };
        }

        /// <summary>
        /// Deconstruct a LineLoad.
        /// </summary>
        /// <param name="lineTemperatureLoad">LineLoad.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Curve", "Direction", "TopBotLocVal1", "TopBotLocVal2", "LoadCaseGuid", "Comment"})]
        public static Dictionary<string, object> LineTemperatureLoadDeconstruct(FemDesign.Loads.LineTemperatureLoad lineTemperatureLoad)
        {
            return new Dictionary<string, object>
            {
                {"Guid", lineTemperatureLoad.Guid},
                {"Curve", lineTemperatureLoad.Edge.ToDynamo()},
                {"Direction", lineTemperatureLoad.Direction.ToDynamo()},
                {"TopBotLocVal1", lineTemperatureLoad.TopBotLocVal[0]},
                {"TopBotLocVal2", lineTemperatureLoad.TopBotLocVal[1]},
                {"LoadCaseGuid", lineTemperatureLoad.LoadCase},
                {"Comment", lineTemperatureLoad.Comment}
            };
        }

        /// <summary>
        /// Deconstruct a TopBottomLocationValue element.
        /// </summary>
        /// <param name="topBotLocVal">TopBottomLocationValue</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Point", "TopValue", "BottomValue"})]
        public static Dictionary<string, object> TopBottomLocationValueDeconstruct(FemDesign.Loads.TopBotLocationValue topBotLocVal)
        {
            return new Dictionary<string, object>
            {
                {"Point", topBotLocVal.GetFdPoint().ToDynamo()},
                {"TopValue", topBotLocVal.TopVal},
                {"BottomValue", topBotLocVal.BottomVal}
            };
        }

        /// <summary>
        /// Deconstruct a SurfaceLoad.
        /// </summary>
        /// <param name="surfaceLoad">SurfaceLoad.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Type", "Surface", "Direction", "q1", "q2", "q3", "LoadCaseGuid", "Comment"})]
        public static Dictionary<string, object> SurfaceLoadDeconstruct(FemDesign.Loads.SurfaceLoad surfaceLoad)
        {
            if (surfaceLoad.Loads.Count == 1)
            {
                return new Dictionary<string, object>
                {
                    {"Guid", surfaceLoad.Guid},
                    {"Type", surfaceLoad.LoadType},
                    {"Surface", surfaceLoad.Region.ToDynamoSurface()},
                    {"Direction", surfaceLoad.Direction.ToDynamo()},
                    {"q1", surfaceLoad.Loads[0].Value},
                    {"q2", surfaceLoad.Loads[0].Value},
                    {"q3", surfaceLoad.Loads[0].Value},
                    {"LoadCaseGuid", surfaceLoad.LoadCase},
                    {"Comment", surfaceLoad.Comment}
                };
            }
            else if (surfaceLoad.Loads.Count == 3)
            {
                return new Dictionary<string, object>
                {
                    {"Guid", surfaceLoad.Guid},
                    {"Type", surfaceLoad.LoadType},
                    {"Surface", surfaceLoad.Region.ToDynamoSurface()},
                    {"Direction", surfaceLoad.Direction.ToDynamo()},
                    {"q1", surfaceLoad.Loads[0].Value},
                    {"q2", surfaceLoad.Loads[1].Value},
                    {"q3", surfaceLoad.Loads[2].Value},
                    {"LoadCaseGuid", surfaceLoad.LoadCase},
                    {"Comment", surfaceLoad.Comment}
                };
            }
            else
            {
                throw new System.ArgumentException("Length of load must be 1 or 3.");
            }  
        }

        /// <summary>
        /// Deconstruct a SurfaceTemperatureLoad.
        /// </summary>
        /// <param name="srfTmpLoad">SurfaceTemperatureLoad.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Surface", "TopBotLocVal1", "TopBotLocVal2", "TopBotLocVal3", "LoadCaseGuid", "Comment"})]
        public static Dictionary<string, object> SurfaceTemperatureLoadDeconstruct(FemDesign.Loads.SurfaceTemperatureLoad srfTmpLoad)
        {
            if (srfTmpLoad.TopBotLocVal.Count == 1)
            {
                return new Dictionary<string, object>
                {
                    {"Guid", srfTmpLoad.Guid},
                    {"Surface", srfTmpLoad.Region.ToDynamoSurface()},
                    {"TopBotLocVal1", srfTmpLoad.TopBotLocVal[0]},
                    {"TopBotLocVal2", srfTmpLoad.TopBotLocVal[0]},
                    {"TopBotLocVal3", srfTmpLoad.TopBotLocVal[0]},
                    {"LoadCaseGuid", srfTmpLoad.LoadCase},
                    {"Comment", srfTmpLoad.Comment}
                };
            }
            else if (srfTmpLoad.TopBotLocVal.Count == 3)
            {
                return new Dictionary<string, object>
                {
                    {"Guid", srfTmpLoad.Guid},
                    {"Surface", srfTmpLoad.Region.ToDynamoSurface()},
                    {"TopBotLocVal1", srfTmpLoad.TopBotLocVal[0]},
                    {"TopBotLocVal2", srfTmpLoad.TopBotLocVal[1]},
                    {"TopBotLocVal3", srfTmpLoad.TopBotLocVal[2]},
                    {"LoadCaseGuid", srfTmpLoad.LoadCase},
                    {"Comment", srfTmpLoad.Comment}
                };
            }
            else
            {
                throw new System.ArgumentException("Length of load must be 1 or 3.");
            }
            
        }

        /// <summary>
        /// Deconstruct a PressureLoad.
        /// </summary>
        /// <param name="pressureLoad">PressureLoad.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Type", "Surface", "Direction", "z0", "q0", "qh", "LoadCaseGuid", "Comment"})]
        public static Dictionary<string, object> PressureLoadDeconstruct(FemDesign.Loads.PressureLoad pressureLoad)
        {
            return new Dictionary<string, object>
            {
                {"Guid", pressureLoad.Guid},
                {"Type", pressureLoad.LoadType},
                {"Surface", pressureLoad.Region.ToDynamoSurface()},
                {"Direction", pressureLoad.Direction.ToDynamo()},
                {"z0", pressureLoad.Z0},
                {"q0", pressureLoad.Q0},
                {"qh", pressureLoad.Qh},
                {"LoadCaseGuid", pressureLoad.LoadCase},
                {"Comment", pressureLoad.Comment}
            };
        }

        /// <summary>
        /// Deconstruct a LoadCase.
        /// </summary>
        /// <param name="loadCase">LoadCase.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Name", "Type", "DurationClass"})]
        public static Dictionary<string, object> LoadCaseDeconstruct(FemDesign.Loads.LoadCase loadCase)
        {
            return new Dictionary<string, object>
            {
                {"Guid", loadCase.Guid},
                {"Name", loadCase.Name},
                {"Type", loadCase.Type},
                {"DurationClass", loadCase.DurationClass}
            };
        }
        
        /// <summary>
        /// Deconstruct a LoadCombination.
        /// </summary>
        /// <param name="loadCombination">LoadCombination.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Name", "LoadCases", "Gammas"})]
        public static Dictionary<string, object> LoadCombinationDeconstruct(FemDesign.Loads.LoadCombination loadCombination)
        {
            return new Dictionary<string, object>
            {
                {"Guid", loadCombination.Guid},
                {"Name", loadCombination.Name},
                {"LoadCases", loadCombination.GetLoadCaseGuidsAsString()},
                {"Gammas", loadCombination.GetGammas()}
            };
        }

        /// <summary>
        /// Deconstruct basic material information
        /// </summary>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Standard", "Country", "Name"})]
        public static Dictionary<string, object> MaterialDeconstruct(FemDesign.Materials.Material material)
        {
            return new Dictionary<string, object>
            {
                {"Guid", material.Guid},
                {"Standard", material.Standard},
                {"Country", material.Country},
                {"Name", material.Name}
            };
        }

        /// <summary>
        /// Deconstruct model.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "CountryCode", "Bars", "FictitiousBars", "Shells", "FictitiousShells", "Covers", "Loads", "LoadCases", "LoadCombinations", "Supports", "Axes", "Storeys"})]
        public static Dictionary<string, object> ModelDeconstruct(FemDesign.Model model)
        {
            List<StructureGrid.Axis> axes;
            if (model.Entities.Axes != null)
            {
                axes = model.Entities.Axes.Axis;
            }
            else
            {
                axes = null;
            }

            List<StructureGrid.Storey> storeys;
            if (model.Entities.Storeys != null)
            {
                storeys = model.Entities.Storeys.Storey;
            }
            else
            {
                storeys = null;
            }
        
            // return
            return new Dictionary<string, object>
            {
                {"Guid", model.Guid},
                {"CountryCode", model.Country},
                {"Bars", model.GetBars()},
                {"FictitiousBars", model.Entities.AdvancedFem.FictitiousBar},
                {"Shells", model.GetSlabs()},
                {"FictitiousShells", model.GetFictitiousShells()},
                {"Covers", model.Entities.AdvancedFem.Cover},
                {"Loads", model.Entities.Loads.GetLoads()},
                {"LoadCases", model.Entities.Loads.LoadCases},
                {"LoadCombinations", model.Entities.Loads.LoadCombinations},
                {"Supports", model.Entities.Supports.ListSupports()},
                {"Axes", axes},
                {"Storeys", storeys}
            };
        }

        /// <summary>
        /// Deconstruct a SurfaceReinforcement.
        /// </summary>
        /// <param name="surfaceReinforcement">SurfaceReinforcement</param>
        /// <returns></returns>
        [MultiReturn(new[]{"Guid", "Straight", "Wire", "Surface"})]
        [IsVisibleInDynamoLibrary(true)]
        public static Dictionary<string, object> SurfaceReinforcementDeconstruct(FemDesign.Reinforcement.SurfaceReinforcement surfaceReinforcement)
        {
            return new Dictionary<string, object>
            {
                {"Guid", surfaceReinforcement.Guid},
                {"Straight", surfaceReinforcement.Straight},
                {"Wire", surfaceReinforcement.Wire},
                {"Surface", surfaceReinforcement.Region.ToDynamoSurface()}
            };
        }

        /// <summary>
        /// Deconstruct a SurfaceReinforcement Parameters
        /// </summary>
        /// <param name="surfaceReinforcement">SurfaceReinforcement</param>
        /// <returns></returns>
        [MultiReturn(new[]{"Guid", "SingleLayerReinforcement", "XDirection", "YDirection"})]
        [IsVisibleInDynamoLibrary(true)]
        public static Dictionary<string, object> SurfaceReinforcementParametersDeconstruct(FemDesign.Reinforcement.SurfaceReinforcementParameters surfaceReinforcementParameters)
        {
            return new Dictionary<string, object>
            {
                {"Guid", surfaceReinforcementParameters.Guid},
                {"SingleLayerReinforcement", surfaceReinforcementParameters.SingleLayerReinforcement},
                {"XDirection", surfaceReinforcementParameters.XDirection.ToDynamo()},
                {"YDirection", surfaceReinforcementParameters.YDirection.ToDynamo()}
            };
        }

        /// <summary>
        /// Deconstruct a Wire.
        /// </summary>
        /// <param name="wire">Wire.</param>
        /// <returns></returns>
        [MultiReturn(new[]{"Diameter", "ReinforcingMaterial", "Profile"})]
        [IsVisibleInDynamoLibrary(true)]
        public static Dictionary<string, object> WireDeconstruct(FemDesign.Reinforcement.Wire wire)
        {
            return new Dictionary<string, object>
            {
                {"Diameter", wire.Diameter},
                {"ReinforcingMaterial", wire.ReinforcingMaterialGuid},
                {"Profile", wire.Profile}
            };
        }

        /// <summary>
        /// Deconstruct a storey element.
        /// </summary>
        /// <param name="storey">Storey.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Origo", "Direction", "DimensionX", "DimensionY", "Name"})]
        public static Dictionary<string, object> StoreyDeconstruct(FemDesign.StructureGrid.Storey storey)
        {
            return new Dictionary<string, object>
            {
                {"Guid", storey.Guid},
                {"Origo", storey.Origo.ToDynamo()},
                {"Direction", storey.Direction.ToDynamo()},
                {"DimensionX", storey.DimensionX},
                {"DimensionY", storey.DimensionY},
                {"Name", storey.Name}
            };
        }

        /// <summary>
        /// Deconstruct a Straight.
        /// </summary>
        /// <param name="straight">Straight.</param>
        /// <returns></returns>
        [MultiReturn(new[]{"Direction", "Space", "Face", "Cover"})]
        [IsVisibleInDynamoLibrary(true)]
        public static Dictionary<string, object> StraightDeconstruct(FemDesign.Reinforcement.Straight straight)
        {
            return new Dictionary<string, object>
            {
                {"Direction", straight.Direction},
                {"Space", straight.Space},
                {"Face", straight.Face},
                {"Cover", straight.Cover}
            };
        }

        /// <summary>
        /// Deconstruct a slab element.
        /// </summary>
        /// <param name="slab">Slab.</param>
        /// <returns></returns>
        [MultiReturn(new[]{"Guid", "Surface", "ThicknessItems", "Material", "ShellEccentricity", "ShellOrthotropy", "EdgeCurves", "ShellEdgeConnections", "LocalX", "LocalY", "SurfaceReinforcementParameters", "SurfaceReinforcement", "Identifier"})]
        [IsVisibleInDynamoLibrary(true)]
        public static Dictionary<string, object> SlabDeconstruct(FemDesign.Shells.Slab slab)
        {
            return new Dictionary<string, object>
            {
                {"Guid", slab.Guid},
                {"Surface", slab.SlabPart.Region.ToDynamoSurface()},
                {"ThicknessItems", slab.SlabPart.Thickness},
                {"Material", slab.Material},
                {"ShellEccentricity", slab.SlabPart.ShellEccentricity},
                {"ShellOrthotropy", slab.SlabPart.ShellOrthotropy},
                {"EdgeCurves", slab.SlabPart.Region.ToDynamoCurves()},
                {"ShellEdgeConnections", slab.SlabPart.GetEdgeConnections()},
                {"LocalX", slab.SlabPart.LocalX.ToDynamo()},
                {"LocalY", slab.SlabPart.LocalY.ToDynamo()},
                {"SurfaceReinforcementParameters", slab.SurfaceReinforcementParameters},
                {"SurfaceReinforcement", slab.SurfaceReinforcement},
                {"Identifier", slab.Name}
            };
        }

        /// <summary>
        /// Deconstruct a fictitious shell element.
        /// </summary>
        /// <param name="fictitiousShell">FictitiousShell.</param>
        /// <returns></returns>
        [MultiReturn(new[]{"Guid", "AnalyticalId", "Surface", "MembraneStiffness", "FlexuralStiffness", "ShearStiffness", "Density", "T1", "T2", "Alpha1", "Alpha2", "IgnoreInStImpCalc", "EdgeCurves", "ShellEdgeConnections", "LocalX", "LocalY"})]
        [IsVisibleInDynamoLibrary(true)]
        public static Dictionary<string, object> FictitiousShellDeconstruct(FemDesign.ModellingTools.FictitiousShell fictitiousShell)
        {
            return new Dictionary<string, object>
            {
                {"Guid", fictitiousShell.Guid},
                {"AnalyticalId", fictitiousShell.Identifier},
                {"Surface", fictitiousShell.Region.ToDynamoSurface()},
                {"MembraneStiffness", fictitiousShell.MembraneStiffness},
                {"FlexuralStiffness", fictitiousShell.FlexuralStiffness},
                {"ShearStiffness", fictitiousShell.ShearStiffness},
                {"Density", fictitiousShell.Density},
                {"T1", fictitiousShell.T1},
                {"T2", fictitiousShell.T2},
                {"Alpha1", fictitiousShell.Alpha1},
                {"Alpha2", fictitiousShell.Alpha2},
                {"IgnoreInStImpCalc", fictitiousShell.IgnoreInStImpCalculation},
                {"EdgeCurves", fictitiousShell.Region.ToDynamoCurves()},
                {"ShellEdgeConnections", fictitiousShell.Region.GetEdgeConnections()},
                {"LocalX", fictitiousShell.LocalX.ToDynamo()},
                {"LocalY", fictitiousShell.LocalY.ToDynamo()}
            };
        }

        /// <summary>
        /// Deconstruct a section
        /// </summary>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "Name", "Surfaces", "SectionType", "MaterialType", "GroupName", "TypeName", "SizeName"})]
        public static Dictionary<string, object> SectionDeconstruct(FemDesign.Sections.Section section)
        {
            return new Dictionary<string, object>
            {
                {"Guid", section.Guid},
                {"Name", section.Name},
                {"Surfaces", section.RegionGroup.ToDynamo()},
                {"SectionType", section.Type},
                {"MaterialType", section.MaterialType},
                {"GroupName", section.GroupName},
                {"TypeName", section.TypeName},
                {"SizeName", section.SizeName}
            };
        }

        /// <summary>
        /// Deconstruct a ShellEdgeConnection.
        /// </summary>
        /// <param name="shellEdgeConnection">ShellEdgeConnection.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[] {"Guid", "AnalyticalID", "PredefinedName", "PredefinedGuid", "Friction", "Motions", "Rotations"})]
        public static Dictionary<string, object> ShellEdgeConnectionDeconstruct(FemDesign.Shells.ShellEdgeConnection shellEdgeConnection)
        {
            if (shellEdgeConnection == null)
            {
                return new Dictionary<string, object>
                {
                    {"Guid", null},
                    {"AnalyticalID", null},
                    {"PredefinedName", null},
                    {"PredefinedGuid", null},
                    {"Friction", null},
                    {"Motions", null},
                    {"Rotations", null},
                };
            }
            else
            {
                if (shellEdgeConnection.Rigidity != null && shellEdgeConnection.Rigidity._friction == null)
                {
                    return new Dictionary<string, object>
                    {
                        {"Guid", shellEdgeConnection.Guid},
                        {"AnalyticalID", shellEdgeConnection.Name},
                        {"PredefinedName", null},
                        {"PredefinedGuid", null},
                        {"Friction", null},
                        {"Motions", shellEdgeConnection.Rigidity.Motions},
                        {"Rotations", shellEdgeConnection.Rigidity.Rotations},
                    };
                }
                else if (shellEdgeConnection.Rigidity != null && shellEdgeConnection.Rigidity._friction != null)
                {
                    return new Dictionary<string, object>
                    {
                        {"Guid", shellEdgeConnection.Guid},
                        {"AnalyticalID", shellEdgeConnection.Name},
                        {"PredefinedName", null},
                        {"PredefinedGuid", null},
                        {"Friction", shellEdgeConnection.Rigidity.Friction},
                        {"Motions", shellEdgeConnection.Rigidity.Motions},
                        {"Rotations", shellEdgeConnection.Rigidity.Rotations},
                    };
                }
                else if (shellEdgeConnection.PredefRigidity != null && shellEdgeConnection.PredefRigidity.Rigidity._friction == null)
                {
                    return new Dictionary<string, object>
                    {
                        {"Guid", shellEdgeConnection.Guid},
                        {"AnalyticalID", shellEdgeConnection.Name},
                        {"PredefinedName", shellEdgeConnection.PredefRigidity.Name},
                        {"PredefinedGuid", shellEdgeConnection.PredefRigidity.Guid},
                        {"Friction", null},
                        {"Motions", shellEdgeConnection.PredefRigidity.Rigidity.Motions},
                        {"Rotations", shellEdgeConnection.PredefRigidity.Rigidity.Rotations},
                    };   
                }
                else if (shellEdgeConnection.PredefRigidity != null && shellEdgeConnection.PredefRigidity.Rigidity._friction != null)
                {
                    return new Dictionary<string, object>
                    {
                        {"Guid", shellEdgeConnection.Guid},
                        {"AnalyticalID", shellEdgeConnection.Name},
                        {"PredefinedName", shellEdgeConnection.PredefRigidity.Name},
                        {"PredefinedGuid", shellEdgeConnection.PredefRigidity.Guid},
                        {"Friction", shellEdgeConnection.PredefRigidity.Rigidity.Friction},
                        {"Motions", shellEdgeConnection.PredefRigidity.Rigidity.Motions},
                        {"Rotations", shellEdgeConnection.PredefRigidity.Rigidity.Rotations},
                    };   
                }
                else
                {
                    throw new System.ArgumentException("Unexpected shell edge connection");
                }
            }       
        }

        /// <summary>
        /// Deconstruct a shear stiffness matrix, stiffness matrix 2 type
        /// </summary>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"XZ", "YZ"})]
        public static Dictionary<string, object> StiffnessMatrix2Type(ModellingTools.StiffnessMatrix2Type stiffnessMatrix)
        {
            return new Dictionary<string, object>
            {
                {"XZ", stiffnessMatrix.XZ},
                {"YZ", stiffnessMatrix.YZ}
            };
        }

        /// <summary>
        /// Deconstruct a membrane or flexural stiffness matrix, stiffness matrix 4 type
        /// </summary>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"XX", "XY", "YY", "GXY"})]
        public static Dictionary<string, object> StiffnessMatrix4Type(ModellingTools.StiffnessMatrix4Type stiffnessMatrix)
        {
            return new Dictionary<string, object>
            {
                {"XX", stiffnessMatrix.XX},
                {"XY", stiffnessMatrix.XY},
                {"YY", stiffnessMatrix.YY},
                {"GXY", stiffnessMatrix.GXY}
            };
        }

        /// <summary>
        /// Deconstruct a Motions or Rotations element.
        /// </summary>
        /// <param name="release">Motions or Rotations.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"x_neg", "x_pos", "y_neg", "y_pos", "z_neg", "z_pos"})]
        public static Dictionary<string, object> ReleaseDeconstruct(object release)
        {
            //
            if (release == null)
            {
                return null;
            }

            //
            if (release.GetType() == typeof(FemDesign.Releases.Motions))
            {
                var obj = (FemDesign.Releases.Motions)release;
                return new Dictionary<string, object>
                {
                    {"x_neg", obj.XNeg},
                    {"x_pos", obj.XPos},
                    {"y_neg", obj.YNeg},
                    {"y_pos", obj.YPos},
                    {"z_neg", obj.ZNeg},
                    {"z_pos", obj.ZPos}
                };
            }
            else if (release.GetType() == typeof(FemDesign.Releases.Rotations))
            {
                var obj = (FemDesign.Releases.Rotations)release;
                return new Dictionary<string, object>
                {
                    {"x_neg", obj.XNeg},
                    {"x_pos", obj.XPos},
                    {"y_neg", obj.YNeg},
                    {"y_pos", obj.YPos},
                    {"z_neg", obj.ZNeg},
                    {"z_pos", obj.ZPos}
                };
            }
            else
            {
                throw new System.ArgumentException("Type is not supported. ReleaseDeconstruct failed.");
            }
        }

        /// <summary>
        /// Deconstruct a Support element.
        /// </summary>
        /// <param name="support">PointSupport or LineSupport</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[]{"Guid", "AnalyticalID", "Geometry", "MovingLocal", "LocalX", "LocalY", "Motions", "Rotations"})]
        public static Dictionary<string, object> SupportDeconstruct(object support)
        {
            if (support.GetType() == typeof(FemDesign.Supports.PointSupport))
            {
                var obj = (FemDesign.Supports.PointSupport)support;
                return new Dictionary<string, object>
                {
                    {"Guid", obj.Guid},
                    {"AnalyticalID", obj.Name},
                    {"Geometry", obj.GetDynamoGeometry()},
                    {"MovingLocal", "PointLoad has no moving local property."},
                    {"LocalX", obj.Group.LocalX.ToDynamo()},
                    {"LocalY", obj.Group.LocalY.ToDynamo()},
                    {"Motions", obj.Group.Rigidity.Motions},
                    {"Rotations", obj.Group.Rigidity.Rotations}
                };
            }
            else if (support.GetType() == typeof(FemDesign.Supports.LineSupport))
            {
                var obj = (FemDesign.Supports.LineSupport)support;
                return new Dictionary<string, object>
                {
                    {"Guid", obj.Guid},
                    {"AnalyticalID", obj.Name},
                    {"Geometry", obj.GetDynamoGeometry()},
                    {"MovingLocal", obj.MovingLocal},
                    {"LocalX", obj.Group.LocalX.ToDynamo()},
                    {"LocalY", obj.Group.LocalY.ToDynamo()},
                    {"Motions", obj.Group.Rigidity.Motions},
                    {"Rotations", obj.Group.Rigidity.Rotations}
                };
            }
            else if (support.GetType() == typeof(FemDesign.Supports.SurfaceSupport))
            {
                var obj = (FemDesign.Supports.SurfaceSupport)support;
                return new Dictionary<string, object>
                {
                    {"Guid", obj.Guid},
                    {"AnalyticalID", obj.Identifier},
                    {"Geometry", obj.Region.ToDynamoSurface()},
                    {"MovingLocal", "SurfaceSupport has no moving local property."},
                    {"LocalX", obj.CoordinateSystem.LocalX.ToDynamo()},
                    {"LocalY", obj.CoordinateSystem.LocalY.ToDynamo()},
                    {"Motions", obj.Rigidity.Motions},
                    {"Rotations", "SurfaceSupport has no rotations property."}
                };
            }
            else
            {
                throw new System.ArgumentException("Type is not supported. SupportDeconstruct failed.");
            }
        }
    }
}
#endregion