﻿namespace Supercell.Life.Server.Protocol.Commands.Client
{
    using Supercell.Life.Titan.DataStream;

    using Supercell.Life.Server.Helpers;
    using Supercell.Life.Server.Network;

    internal class LogicFinishHeroUpgradeCommand : LogicCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogicFinishHeroUpgradeCommand"/> class.
        /// </summary>
        public LogicFinishHeroUpgradeCommand(Connection connection, ByteStream stream) : base(connection, stream)
        {
            // LogicFinishHeroUpgradeCommand.
        }

        internal override void Decode()
        {
            this.ReadHeader();
        }

        internal override void Execute()
        {
            this.Connection.Avatar.HeroUpgrade.Finish();
            this.Connection.Avatar.Save();
        }
    }
}
