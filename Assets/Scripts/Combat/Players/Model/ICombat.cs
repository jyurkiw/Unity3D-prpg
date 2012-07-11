/**
 * Required interface for basic combat.
 */
public interface ICombat {
	int Hits { get; set; }
	int Energy { get; set; }
	int Attack { get; set; }
	int Defense { get; set; }
	int Special { get; set; }
	float Speed { get; set; }
}
