using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GigHub.Tests.Domain.Models
{
    [TestClass]
    public class NotificationTests
    {
        [TestMethod]
        public void GigCanceled_WhenCalled_ShouldReturnedANotificationForACanceledGig()
        {
            var gig = new Gig();

            var notification = Notification.GigCanceled(gig);

            // Again, here, we have two assertions, but that doesn't mean we're
            // violating the single responsibility principle. We're verifying 
            // one logical fact: that upon calling Notification.GigCanceled()
            // we'll get a notification object for the canceled gig. This notification
            // object should be in the right state, meaning its type should be
            // GigCanceled and its gig should be the gig for each we created 
            // the notification. 

            notification.Type.Should().Be(NotificationType.GigCanceled);
            notification.Gig.Should().Be(gig);
        }
    }
}
