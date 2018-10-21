using System;
using System.Collections.Generic;
using System.Linq;
using MyOrganizationApp.Models;

namespace MyOrganizationApp.DB
{
    public class SubscriberDAO
    {
        private static List<Subscriber> _subscribers;

        public SubscriberDAO()
        {
            _subscribers = new List<Subscriber>();
        }

        private bool Contains(Subscriber subscriber)
        {
            foreach (Subscriber s in _subscribers)
            {
                if (s.EMail.Equals(subscriber.EMail))
                    return true;
            }

            return false;
        }

        public bool Insert(Subscriber subscriber)
        {
            if (subscriber == null || Contains(subscriber))
                return false;

            _subscribers.Add(subscriber);

            return true;
        }
    }
}
