namespace HotelReservation.Cancellation;

/// <summary>Résout une politique par nom sans switch dans CancellationService (OCP).</summary>
public class CancellationPolicyRegistry
{
    private readonly Dictionary<string, ICancellationPolicy> _policies;

    public CancellationPolicyRegistry(IEnumerable<ICancellationPolicy> policies)
    {
        _policies = policies.ToDictionary(p => p.PolicyName, StringComparer.OrdinalIgnoreCase);
    }

    public ICancellationPolicy Get(string policyName)
    {
        if (!_policies.TryGetValue(policyName, out var policy))
            throw new ArgumentException($"Unknown cancellation policy: {policyName}");
        return policy;
    }
}
