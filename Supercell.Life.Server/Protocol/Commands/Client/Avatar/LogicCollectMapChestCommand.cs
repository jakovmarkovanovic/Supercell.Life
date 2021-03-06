﻿namespace Supercell.Life.Server.Protocol.Commands.Client
{
    using Supercell.Life.Titan.DataStream;

    using Supercell.Life.Server.Helpers;
    using Supercell.Life.Server.Logic.Game.Objects;
    using Supercell.Life.Server.Network;

    internal class LogicCollectMapChestCommand : LogicCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogicCollectMapChestCommand"/> class.
        /// </summary>
        public LogicCollectMapChestCommand(Connection connection, ByteStream stream) : base(connection, stream)
        {
            // LogicCollectMapChestCommand.
        }

        internal override void Decode()
        {
            this.ReadHeader();
        }

        internal override void Execute()
        {
            LogicChest chest = new LogicChest(this.Connection.Avatar);
            chest.CreateMapChest();
            
            this.Connection.Avatar.Save();
        }
    }
}
