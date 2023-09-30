redirectToCheckout = function (sessionId) {
    var stripe = Stripe("sk_test_51NcPvkLnysNd4PuIN3QpZAfIUkLqwvdNQohJXeMB5avGiiSjzQIl6e7zg4U0on4SXFQ0dJ8IqAH5iOmXNIIByiWe00TBysNh2E");
    stripe.redirectToCheckout({ sessionId: sessionId });
}