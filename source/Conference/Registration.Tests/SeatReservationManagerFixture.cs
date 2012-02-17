﻿// ==============================================================================================================
// Microsoft patterns & practices
// CQRS Journey project
// ==============================================================================================================
// Copyright (c) Microsoft Corporation and contributors http://cqrsjourney.github.com/contributors/members
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.
// ==============================================================================================================

namespace Registration.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Context
    {
        private static readonly Guid TicketTypeId = Guid.NewGuid();

        public SeatReservationManager given_available_seats()
        {
            var sut = new SeatReservationManager(TicketTypeId);
            sut.AddSeats(10);
            return sut;
        }

        public SeatReservationManager given_some_avilable_seats_and_some_taken()
        {
            var sut = this.given_available_seats();
            sut.MakeReservation(Guid.NewGuid(), 6);
            return sut;
        }

        [TestMethod]
        public void when_reserving_less_seats_than_total_then_succeeds()
        {
            var sut = this.given_available_seats();
            sut.MakeReservation(Guid.NewGuid(), 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void when_reserving_more_seats_than_total_then_fails()
        {
            var sut = this.given_available_seats();
            sut.MakeReservation(Guid.NewGuid(), 11);
        }

        [TestMethod]
        public void when_reserving_less_seats_than_remaining_then_fails()
        {
            var sut = this.given_some_avilable_seats_and_some_taken();
            sut.MakeReservation(Guid.NewGuid(), 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void when_reserving_more_seats_than_remaining_then_fails()
        {
            var sut = this.given_some_avilable_seats_and_some_taken();
            sut.MakeReservation(Guid.NewGuid(), 5);
        }
    }
}
