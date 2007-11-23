using System;
using System.Windows.Forms;
using APE;

namespace Demo
{
	public abstract class AbstractDemo
	{
		public abstract void Run();

		public virtual void HandleKeyDown(KeyEventArgs e) { }
		public virtual void HandleKeyUp(KeyEventArgs e) { }

		public abstract void Paint(IRenderer renderer);
	}
}
