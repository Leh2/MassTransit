﻿// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Tests.Serialization
{
    using System.Threading.Tasks;
    using MassTransit.Serialization;
    using NUnit.Framework;
    using TestFramework;
    using TestFramework.Messages;


    [TestFixture]
    public class SeparateSerializer_Specs :
        InMemoryTestFixture
    {
        [Test]
        public async Task Should_handle_both_serializers()
        {
            Task<ConsumeContext<PongMessage>> ponged = SubscribeHandler<PongMessage>();

            Bus.Publish(new PingMessage());

            ConsumeContext<PingMessage> pingContext = await _handled;

            Assert.That(pingContext.ReceiveContext.ContentType, Is.EqualTo(JsonMessageSerializer.JsonContentType));

            ConsumeContext<PongMessage> pongContext = await ponged;

            Assert.That(pongContext.ReceiveContext.ContentType, Is.EqualTo(BsonMessageSerializer.BsonContentType));
        }

        Task<ConsumeContext<PingMessage>> _handled;

        protected override void ConfigureInputQueueEndpoint(IInMemoryReceiveEndpointConfigurator configurator)
        {
            base.ConfigureInputQueueEndpoint(configurator);

            configurator.UseBsonSerializer();

            _handled = Handler<PingMessage>(configurator, async context =>
            {
                await context.RespondAsync(new PongMessage(context.Message.CorrelationId));
            });
        }
    }
}