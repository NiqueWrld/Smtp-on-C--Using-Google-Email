# Send Email Project

## Description

This project demonstrates how to send an email using **MailKit** in C#. It uses **SMTP** to send a message through Gmail's SMTP server. It also includes the setup for generating and using an **App Password** for Gmail.

---

## Requirements

* **.NET 5** or higher
* **MailKit** NuGet package
* A **Gmail account** with **2-Step Verification** enabled (for generating App Passwords)

---

## Installation

### Step 1: Clone the Repository

You can either clone the project or download the source files directly.

```bash
git clone https://github.com/yourusername/send-email.git
cd send-email
```

### Step 2: Install MailKit

MailKit is a cross-platform mail client library for .NET.

Run the following command to install **MailKit** via **NuGet**:

```bash
dotnet add package MailKit
```

This will automatically add **MailKit** to your project’s dependencies.

---

## Configuration

Before you send an email, you need to configure the SMTP settings in your `EmailSender` class:

```csharp
private string smtpHost = "smtp.gmail.com";
private int smtpPort = 587;  // Port for TLS (587)
private string smtpUser = "your_email@gmail.com"; // Your Gmail address
private string smtpPass = "your_app_password";    // Your Gmail App Password
```

### Step 3: Generate Gmail App Password

1. Go to: [https://myaccount.google.com/apppasswords](https://myaccount.google.com/apppasswords)
2. Sign in to your Google account if prompted.
3. Under **"Select App"**, choose **Other (Custom name)** and give it a name (e.g., `EmailApp`).
4. Click **Generate**. Copy the generated **16-character App Password**.
5. Use the **App Password** (not your Gmail password) in the code above for the `smtpPass`.

---

## Usage

Once you've installed the dependencies and configured your Gmail account, you can run the application to send an email.

### Step 4: Run the Application

Run the application using:

```bash
dotnet run
```

This will send an email to the recipient you specified in the `Main` method of the `Program.cs`.

---

## Code Example

Here’s the essential code for sending an email using **MailKit**:

```csharp
using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

class Program
{
    static void Main(string[] args)
    {
        var sender = new EmailSender();
        sender.SendEmail(
            to: "recipient@example.com", // Replace with recipient email
            subject: "Test Email via MailKit",
            body: "This is a test email sent using MailKit."
        );
    }
}

public class EmailSender
{
    private string smtpHost = "smtp.gmail.com";
    private int smtpPort = 587;
    private string smtpUser = "your_email@gmail.com"; // Your Gmail address
    private string smtpPass = "your_app_password";    // Your Gmail App Password

    public void SendEmail(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(smtpUser));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                client.Authenticate(smtpUser, smtpPass);
                client.Send(message);
                client.Disconnect(true);
                Console.WriteLine("Email sent successfully via MailKit!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }
        }
    }
}
```

---

## Troubleshooting

1. **Connection timeout**: Ensure your network isn't blocking the required ports (587 for TLS).
2. **Authentication errors**: Make sure you're using an **App Password** (not your Gmail password).
3. **Firewall or Antivirus blocking**: Some firewalls or antivirus software may block outbound connections to SMTP servers. You may need to disable them temporarily for testing.
