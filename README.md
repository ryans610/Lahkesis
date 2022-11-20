# Lahkesis

Contains two major parts. One is RNGRandom, a random number generator base on System.Security.Cryptography.RNGCryptoServiceProvider that is thread-safe, avoid modulo bias, and safely handle edge cases; The other one is extension methods of all numeric type for System.Random, or whoever inherit from System.Random.
