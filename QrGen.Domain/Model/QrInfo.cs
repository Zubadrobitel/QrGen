﻿using QrGen.Domain.Helpers;

namespace QrGen.Domain.Model
{
    public sealed class QrInfo
    {

        public Guid Id { get; }
        public string Password { get; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; }
        public int GuestCount { get; set; }

        public QrInfo(
            Guid id,
            string password,
            DateTime start,
            DateTime end,
            int guestCount)
        {

            Id = id;
            Password = password;
            Start = start;
            End = end;
            GuestCount = guestCount;
        }

        public static Result<QrInfo> Create(
            Guid id,
            string password,
            DateTime start,
            DateTime end,
            int guestCount)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(password))
                errors.Add("Должен быть указан пароль");
            if (end < start)
                errors.Add("Дата начала не должна быть позже даты окончания");
            if (end < DateTime.UtcNow)
                errors.Add("Дата конца не должна быть в прошлом");
            if (guestCount < 1)
                    errors.Add("Количество гостей не может быть меньше 1");

            if (errors.Count > 0)
                return Result<QrInfo>.Failure(errors);

            var qr = new QrInfo(id, password, start, end, guestCount);
            return Result<QrInfo>.Success(qr);
        }
    }
}
