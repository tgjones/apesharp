using System;

namespace APE
{
	public interface IRenderer
	{
		void InitialiseDeviceContext(object deviceContext);

		void DrawFillRectangle(float x, float y, float w, float h, float angle, float lineThickness, int lineColor, int fillColor);

		void DrawFillCircle(float x, float y, float radius, float lineThickness, int lineColor, int fillColor);
		void DrawCircle(float x, float y, float radius, float lineThickness, int lineColor);
		
		void DrawLine(float angle, float x1, float y1, float x2, float y2, float lineThickness, int lineColor);
	}
}
