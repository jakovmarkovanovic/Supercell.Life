﻿namespace Supercell.Life.Server.Protocol.Commands.Client
{
    using Supercell.Life.Titan.DataStream;

    using Supercell.Life.Server.Files.CsvLogic;
    using Supercell.Life.Server.Helpers;
    using Supercell.Life.Server.Network;

    internal class LogicBuyEnergyPackageCommand : LogicCommand
    {
        internal LogicEnergyPackageData EnergyPackage;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicBuyEnergyPackageCommand"/> class.
        /// </summary>
        public LogicBuyEnergyPackageCommand(Connection connection, ByteStream stream) : base(connection, stream)
        {
            // LogicBuyEnergyPackageCommand.
        }

        internal override void Decode()
        {
            this.EnergyPackage = this.Stream.ReadDataReference<LogicEnergyPackageData>();

            this.ReadHeader();
        }

        internal override void Execute()
        {
            if (this.EnergyPackage != null)
            {
                int alreadyBought = this.Connection.Avatar.EnergyPackages.GetCount(this.EnergyPackage.GlobalID);

                if (this.EnergyPackage.Diamonds.Count > alreadyBought)
                {
                    int cost = this.EnergyPackage.Diamonds[alreadyBought];

                    if (cost > 0)
                    {
                        if (this.Connection.Avatar.Diamonds < cost)
                        {
                            Debugger.Error($"Unable to buy a energy package. {this.Connection.Avatar.Name} does not enough diamonds. (Diamonds : {this.Connection.Avatar.Diamonds}, Require : {cost}).");
                            return;
                        }
                    }

                    this.Connection.Avatar.EnergyPackages.AddItem(this.EnergyPackage.GlobalID, 1);
                    this.Connection.Avatar.EnergyTimer.Stop();
                    
                    this.Connection.Avatar.Diamonds -= cost;
                    this.Connection.Avatar.Energy    = this.Connection.Avatar.MaxEnergy;
                }
                else Debugger.Error("Unable to buy the energy package. The player has already bought all of the packages.");
            }
            else Debugger.Error("Unable to buy the energy package. The package data does not exist or is invalid.");
        }
    }
}