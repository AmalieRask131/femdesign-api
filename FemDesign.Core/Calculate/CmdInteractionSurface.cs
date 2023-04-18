﻿// https://strusoft.com/
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace FemDesign.Calculate
{
    [XmlRoot("cmdinteractionsurface")]
    [System.Serializable]
    public partial class CmdInteractionSurface : CmdCommand
    {
        [XmlAttribute("command")]
        public string Command = "$ CODE_COM INTERACTIONSURFACE"; // token

        /// <summary>
        /// BarPart Guid
        /// </summary>
        [XmlAttribute("guid")]
        public System.Guid Guid { get; set; }

        /// <summary>
        /// cross-section position, measured along the bar from the starting point [m]
        /// </summary>
        [XmlAttribute("offset")]
        public double Offset { get; set; }

        /// <summary>
        /// fUlt is true for Ultimate, false for Accidental or Seismic  combination (different gammaC)
        /// </summary>
        [XmlAttribute("fUlt")]
        public bool fUlt { get; set; }

        [XmlAttribute("outfile")]
        public string FilePath { get; set; }


        /// <summary>
        /// Parameterless constructor for serialization.
        /// </summary>
        private CmdInteractionSurface()
        {

        }
        public CmdInteractionSurface(FemDesign.Bars.Bar bar, string filePath, double offset = 0.0, bool ultimate = true)
        {
            if (bar.BarPart.ComplexMaterialObj.Family != Materials.Family.Concrete)
                throw new System.ArgumentException("Bar must have a concrete section");

            if (offset > bar.BarPart.Edge.Length)
                throw new System.ArgumentException($"Offset can not be larger than {bar.BarPart.Edge.Length}");

            this.Guid = bar.BarPart.Guid;
            this.Offset = offset;
            this.fUlt = ultimate;
            this.FilePath = Path.GetFullPath(filePath);
        }

        public override XElement ToXElement()
        {
            return Extension.ToXElement<CmdInteractionSurface>(this);
        }
    }


}