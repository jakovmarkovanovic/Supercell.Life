﻿namespace Supercell.Life.Server.Protocol.Commands.Debugs.Chat
{
    using Supercell.Life.Titan.Logic.Enums;
    using Supercell.Life.Titan.Logic.Utils;

    using Supercell.Life.Server.Helpers;
    using Supercell.Life.Server.Logic.Slots;
    using Supercell.Life.Server.Network;
    using Supercell.Life.Server.Protocol.Messages;
    using Supercell.Life.Server.Protocol.Messages.Server;

    internal class LogicResourcesCommand : LogicChatCommand
    {
        /// <summary>
        /// Gets the required rank.
        /// </summary>
        internal override Rank RequiredRank
        {
            get
            {
                return Rank.Administrator;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LogicResourcesCommand"/> class.
        /// </summary>
        public LogicResourcesCommand(Connection connection, params string[] parameters) : base(connection, parameters)
        {
            this.Help.AppendLine("Options:");
            this.Help.AppendLine("\t Modes = set,add");
            this.Help.AppendLine("\t Resources = gold,diamonds,xp,score");

            this.Help.AppendLine("Command Usage:");
            this.Help.AppendLine("\t/resource {Mode} {Resource} {Count}");
        }

        internal override void Process()
        {
            if (this.Connection.Avatar.Rank >= this.RequiredRank)
            {
                if (this.Parameters.Length >= 2)
                {
                    if (this.Parameters[0] == "set")
                    {
                        switch (this.Parameters[1])
                        {
                            case "gold":
                            {
                                this.Connection.Avatar.Gold = LogicStringUtil.ConvertToInt(this.Parameters[2]);
                                break;
                            }
                            case "diamonds":
                            {
                                this.Connection.Avatar.Diamonds = LogicStringUtil.ConvertToInt(this.Parameters[2]);
                                break;
                            }
                            case "xp":
                            {
                                this.Connection.Avatar.ExpPoints = LogicStringUtil.ConvertToInt(this.Parameters[2]);
                                break;
                            }
                            case "score":
                            {
                                this.Connection.Avatar.Score = LogicStringUtil.ConvertToInt(this.Parameters[2]);
                                break;
                            }
                            default:
                            {
                                this.Connection.SendChatMessage(this.Help.ToString());
                                break;
                            }
                        }
                    }
                    if (this.Parameters[0] == "add")
                    {
                        switch (this.Parameters[1])
                        {
                            case "gold":
                            {
                                this.Connection.Avatar.AddGold(LogicStringUtil.ConvertToInt(this.Parameters[2]));
                                break;
                            }
                            case "diamonds":
                            {
                                this.Connection.Avatar.AddDiamonds(LogicStringUtil.ConvertToInt(this.Parameters[2]));
                                break;
                            }
                            case "xp":
                            {
                                this.Connection.Avatar.AddXP(LogicStringUtil.ConvertToInt(this.Parameters[2]));
                                break;
                            }
                            case "score":
                            {
                                this.Connection.Avatar.AddTrophies(LogicStringUtil.ConvertToInt(this.Parameters[2]));
                                break;
                            }
                            default:
                            {
                                this.Connection.SendChatMessage(this.Help.ToString());
                                break;
                            }
                        }
                    }

                    this.Connection.SendChatMessage($"Added {this.Parameters[1]} to your {this.Parameters[0]}");
                    this.Connection.Avatar.Save();

                    new OwnAvatarDataMessage(this.Connection).Send();
                }
                else
                {
                    this.Connection.SendChatMessage(this.Help.ToString());
                }
            }
            else
            {
                this.Connection.SendChatMessage("Insufficient privileges.");
            }
        }
    }
}
