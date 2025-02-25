﻿namespace VerifyTests;

public partial class Counter
{
#if NET6_0_OR_GREATER
    Dictionary<DateOnly, string> namedDates;
    Dictionary<TimeOnly, string> namedTimes;
#endif
    Dictionary<DateTime, string> namedDateTimes;
    Dictionary<Guid, string> namedGuids;
    Dictionary<DateTimeOffset, string> namedDateTimeOffsets;
    static AsyncLocal<Counter?> local = new();

    public static Counter Current
    {
        get
        {
            var context = local.Value;
            if (context is null)
            {
                throw new("No current context");
            }

            return context;
        }
    }

    Counter(
#if NET6_0_OR_GREATER
        Dictionary<Date, string> namedDates,
        Dictionary<Time, string> namedTimes,
#endif
        Dictionary<DateTime, string> namedDateTimes,
        Dictionary<Guid, string> namedGuids,
        Dictionary<DateTimeOffset, string> namedDateTimeOffsets)
    {
#if NET6_0_OR_GREATER
        this.namedDates = namedDates;
        this.namedTimes = namedTimes;
#endif
        this.namedDateTimes = namedDateTimes;
        this.namedGuids = namedGuids;
        this.namedDateTimeOffsets = namedDateTimeOffsets;
    }

    internal static Counter Start(
#if NET6_0_OR_GREATER
        Dictionary<Date, string>? namedDates = null,
        Dictionary<Time, string>? namedTimes = null,
#endif
        Dictionary<DateTime, string>? namedDateTimes = null,
        Dictionary<Guid, string>? namedGuids = null,
        Dictionary<DateTimeOffset, string>? namedDateTimeOffsets = null)
    {
        var context = new Counter(
#if NET6_0_OR_GREATER
            namedDates ?? new(),
            namedTimes ?? new(),
#endif
            namedDateTimes ?? new(),
            namedGuids ?? new(),
            namedDateTimeOffsets ?? new());
        local.Value = context;
        return context;
    }

    internal static void Stop() =>
        local.Value = null;
}