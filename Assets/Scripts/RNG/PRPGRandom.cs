// Algorithm attributed to George Marsaglia
// *static unsigned long 
//x=123456789,y=362436069,z=521288629,w=88675123,v=886756453; 
//      /* replace defaults with five random seed values in calling program */ 
//unsigned long xorshift(void) 
//{unsigned long t; 
// t=(x^(x>>7)); x=y; y=z; z=w; w=v; 
// v=(v^(v<<6))^(t^(t<<13)); return (y+y+1)*v;}
// */

/**
 * XORShift PRNG
 * 
 * Only produces positive output.
 */
public class PRPGRandom {
	private ulong a, b, c, d, e, f;
	private ulong rvalue1, rvalue2;
	
	public PRPGRandom(long a, long b, long c, long d, long e) {
		this.a = (ulong)a;
		this.b = (ulong)b;
		this.c = (ulong)c;
		this.d = (ulong)d;
		this.e = (ulong)e;
		rvalue1 = 1;
	}
	
	public PRPGRandom(long[] seed) {
		a = (ulong)seed[0];
		b = (ulong)seed[1];
		c = (ulong)seed[2];
		d = (ulong)seed[3];
		e = (ulong)seed[4];
		rvalue1 = 1;
	}
	
	public void InvertSeeding() {
		a = long.MaxValue - a;
		b = long.MaxValue - b;
		c = long.MaxValue - c;
		d = long.MaxValue - d;
		e = long.MaxValue - e;
	}
	
	private void xorshift() {
		f = (e^(a>>7));
		a = b;
		b = c;
		c = d;
		d = e;
		e = (e^(e<<6))^(f^(f<<13));
		
		rvalue2 = rvalue1;
		rvalue1 = (b+b+1)*e;
	}
	
	/**
	 * Generate a double value between 0 and 1.
	 */
	public double NextDouble() {
		xorshift();
		
		if (rvalue1 >= rvalue2)
			return (double)rvalue2 / (double)rvalue1;
		else
			return (double)rvalue1 / (double)rvalue2;
	}
	
	public float NextFloat() {
		return (float)NextDouble();
	}
	
	public long NextLong() {
		xorshift();
		if (rvalue1 > long.MaxValue)
			return (long)(rvalue1 % long.MaxValue);
		else
			return (long)rvalue1;
	}
	
	public long NextLong(long maxValue) {
		long val = NextLong();
		if (val > maxValue)
			return val % maxValue;
		else
			return val;
	}
	
	public long NextLong(long minValue, long maxValue) {
		return NextLong(maxValue - minValue) + minValue;
	}
	
	public int Next() {
		xorshift();
		if (rvalue1 > int.MaxValue)
			return (int)(rvalue1 % int.MaxValue);
		else
			return (int)rvalue1;
	}
	
	public long[] NextRNGSeedingSet() {
		long[] seed = new long[5];
		for (int x = 0; x < 5; x++)
			seed[x] = NextLong();
		
		return seed;
	}
	
	public int Next(int maxValue) {
		int val = Next();
		if (val > maxValue)
			return val % maxValue;
		else
			return val;
	}
	
	public int Next(int minValue, int maxValue) {
		return Next(maxValue - minValue) + minValue;
	}
}