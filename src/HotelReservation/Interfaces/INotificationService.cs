namespace HotelReservation.Interfaces;

public interface IEmailNotificationSender
{
    void SendEmail(string to, string subject, string body);
}

public interface ISmsNotificationSender
{
    void SendSms(string phoneNumber, string message);
}

public interface IPushNotificationSender
{
    void SendPushNotification(string deviceId, string message);
}

public interface ISlackNotificationSender
{
    void SendSlackMessage(string channel, string message);
}
