using CoordinateSharp;
using Microsoft.CodeAnalysis.Text;
using MissionPlanner.Utilities;
using netDxf;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IronPython.Modules._ast;
using static MissionPlanner.Controls.OpenGLtest2;

namespace MissionPlanner.MV04.HudCrosshairCalc
{
    public partial class TestForm : Form
    {
        private Graphics panelGraphics;

        public TestForm()
        {
            InitializeComponent();
            panelGraphics = panel_CameraView.CreateGraphics();
            comboBox_CrosshairType.SelectedIndex = 0;
        }

        private void panel_CameraView_Paint(object sender, PaintEventArgs e)
        {
            PointLatLngAlt dronePos = new PointLatLngAlt(47.50108956293376, 19.201067584658787, 100);
            PointLatLngAlt targetPos = new PointLatLngAlt(47.49943780818953, 19.203246358954704);
            DateTime now = DateTime.Now;
            int utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(now).Hours;
            Random rnd = new Random();

            RedrawHudElements(new HudElements()
            {
                Time = $"{now.Day.ToString().PadLeft(2, '0')}{now.ToString("MMM", new CultureInfo("en-US")).ToUpperInvariant()}{now.Year}\n{now.ToString("HH:mm:ss")}\nUTC{(utcOffset >= 0 ? "+" : "")}{utcOffset}",
                AGL = $"AGL{rnd.Next(10, 501).ToString().PadLeft(4)}M",
                Velocity = $"VEL{rnd.Next(0, 31).ToString().PadLeft(4)}M/S",
                TGD = $"TGD{rnd.Next(50, 301).ToString().PadLeft(4)}M",
                Battery = $"BAT{rnd.Next(50, 101).ToString().PadLeft(4)}%\n00:29:59",
                SignalStrengths = $"RADIO{rnd.Next(0, 101).ToString().PadLeft(4)}%\nGPS{rnd.Next(0, 101).ToString().PadLeft(6)}%",
                FromOperator = $"OPERATOR{rnd.Next(0, 2001).ToString().PadLeft(5)}M",
                ToWaypoint = $"WAYPOINT{rnd.Next(0, 2001).ToString().PadLeft(5)}M\n00:02:18",
                Camera = $"CAM PITCH{rnd.Next(-91, 0).ToString().PadLeft(5)}°\nYAW{rnd.Next(-359, 361).ToString().PadLeft(7)}°",
                DroneGps = $"UAV{"WGS84".PadLeft(16)}\n{rnd.Next(1000, 10000)}.{rnd.Next(1000, 10000)} {rnd.Next(1000, 10000)}.{rnd.Next(1000, 10000)}",
                TargetGps = $"TRG{"WGS84".PadLeft(16)}\n{rnd.Next(1000, 10000)}.{rnd.Next(1000, 10000)} {rnd.Next(1000, 10000)}.{rnd.Next(1000, 10000)}",
                Crosshairs = (CrosshairsType)comboBox_CrosshairType.SelectedIndex,
                LineDistance = 10,
                LineSpacing = rnd.Next(10, 81)
            });
        }

        private void comboBox_CrosshairType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_CameraView.Invalidate();
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            panel_CameraView.Invalidate();
        }

        /// <summary>
        /// Refreshes the screen
        /// </summary>
        /// <param name="hudElements">Text elements to draw on the screen</param>
        private void RedrawHudElements(HudElements hudElements)
        {
            panel_CameraView.BackColor = Color.Black;
            Font textFont = new Font(FontFamily.GenericMonospace, this.Font.Size);
            SolidBrush textBrush = new SolidBrush(Color.Red);

            // Datetime
            Rectangle Datetime = DrawText(hudElements.Time, textFont, textBrush, new Point(3, 3), RectangleAlign.TopLeft, TextAlign.Left);

            // Battery
            Rectangle Battery = DrawText(hudElements.Battery, textFont, textBrush, new Point(panel_CameraView.Width - 3, 3), RectangleAlign.TopRight, TextAlign.Right);

            int topLeft = Datetime.Right;
            int topStep = ((Battery.Left - topLeft) / 4) / 2;

            // AGL
            DrawText(hudElements.AGL, textFont, textBrush, new Point(topLeft + topStep, 3), RectangleAlign.TopCenter, TextAlign.Left);

            // Velocity
            DrawText(hudElements.Velocity, textFont, textBrush, new Point(topLeft + (3 * topStep), 3), RectangleAlign.TopCenter, TextAlign.Left);

            // TGD
            DrawText(hudElements.TGD, textFont, textBrush, new Point(topLeft + (5 * topStep), 3), RectangleAlign.TopCenter, TextAlign.Left);

            // Signal strengths
            DrawText(hudElements.SignalStrengths, textFont, textBrush, new Point(topLeft + (7 * topStep), 3), RectangleAlign.TopCenter, TextAlign.Right);

            // Camera info
            DrawText(hudElements.Camera, textFont, textBrush, new Point(3, Datetime.Bottom + 20), RectangleAlign.TopLeft, TextAlign.Right);

            // Next waypoint
            Rectangle nextWP = DrawText(hudElements.ToWaypoint, textFont, textBrush, new Point(panel_CameraView.Width - 3, Battery.Bottom + 20), RectangleAlign.TopRight, TextAlign.Right);

            // Operator distance
            DrawText(hudElements.FromOperator, textFont, textBrush, new Point(panel_CameraView.Width - 3, nextWP.Bottom + 20), RectangleAlign.TopRight, TextAlign.Right);

            // Coords
            DrawText(hudElements.DroneGps, textFont, textBrush, new Point(0, panel_CameraView.Height - 3), RectangleAlign.BottomLeft, TextAlign.Left);
            DrawText(hudElements.TargetGps, textFont, textBrush, new Point(panel_CameraView.Width - 3, panel_CameraView.Height - 3), RectangleAlign.BottomRight, TextAlign.Right);

            #region Crosshairs
            int lineHeight = (int)Math.Round(panel_CameraView.Height * 0.1);
            Pen linePen = new Pen(Color.Red, 1);

            if (hudElements.Crosshairs == CrosshairsType.Plus) // Plus
            {
                panelGraphics.DrawLine(linePen,
                    panel_CameraView.Width / 2, panel_CameraView.Height / 2,
                    (panel_CameraView.Width / 2) + lineHeight, panel_CameraView.Height / 2);
                panelGraphics.DrawLine(linePen,
                    panel_CameraView.Width / 2, panel_CameraView.Height / 2,
                    panel_CameraView.Width / 2, (panel_CameraView.Height / 2) + lineHeight);
                panelGraphics.DrawLine(linePen,
                    panel_CameraView.Width / 2, panel_CameraView.Height / 2,
                    (panel_CameraView.Width / 2) - lineHeight, panel_CameraView.Height / 2);
                panelGraphics.DrawLine(linePen,
                    panel_CameraView.Width / 2, panel_CameraView.Height / 2,
                    panel_CameraView.Width / 2, (panel_CameraView.Height / 2) - lineHeight);
            }
            else // Horizontal
            {
                // Draw center ^ character
                panelGraphics.DrawLine(new Pen(Color.Red, 3),
                    panel_CameraView.Width / 2, panel_CameraView.Height / 2,
                    (panel_CameraView.Width / 2) - Math.Min(lineHeight / 2, hudElements.LineSpacing), (panel_CameraView.Height / 2) + lineHeight);
                panelGraphics.DrawLine(new Pen(Color.Red, 3),
                    panel_CameraView.Width / 2, panel_CameraView.Height / 2,
                    (panel_CameraView.Width / 2) + Math.Min(lineHeight / 2, hudElements.LineSpacing), (panel_CameraView.Height / 2) + lineHeight);

                for (int i = 1; i <= 3; i++)
                {
                    // Draw lines to the right
                    panelGraphics.DrawLine(linePen,
                        (panel_CameraView.Width / 2) + (i * hudElements.LineSpacing), panel_CameraView.Height / 2,
                        (panel_CameraView.Width / 2) + (i * hudElements.LineSpacing), (panel_CameraView.Height / 2) + lineHeight);

                    // Draw lines to the left
                    panelGraphics.DrawLine(linePen,
                        (panel_CameraView.Width / 2) - (i * hudElements.LineSpacing), panel_CameraView.Height / 2,
                        (panel_CameraView.Width / 2) - (i * hudElements.LineSpacing), (panel_CameraView.Height / 2) + lineHeight);
                }

                // Draw number under first right line
                DrawText(hudElements.LineDistance.ToString(), textFont, textBrush, new Point((panel_CameraView.Width / 2) + hudElements.LineSpacing, (panel_CameraView.Height / 2) + lineHeight + 3), RectangleAlign.TopCenter, TextAlign.Center);
            }
            #endregion
        }

        private enum RectangleAlign
        {
            TopLeft,
            TopCenter,
            TopRight,
            MiddleLeft,
            MiddleCenter,
            MiddleRight,
            BottomLeft,
            BottomCenter,
            BottomRight
        }

        private enum TextAlign
        {
            Left,
            Center,
            Right
        }

        /// <summary>
        /// Draw a textbox on the screen
        /// </summary>
        /// <param name="text">Text to draw</param>
        /// <param name="font">Font to use on text</param>
        /// <param name="brush">Brust to use on text</param>
        /// <param name="position">Coordinates of the textbox</param>
        /// <param name="rectangleAlignment">Where <paramref name="position"/> is relative to the textbox</param>
        /// <param name="textAlignment">Where to align the text inside the textbox</param>
        /// <param name="drawEdge">Draw the textbox frame</param>
        /// <returns>The textbox rectangle</returns>
        private Rectangle DrawText(string text, Font font, SolidBrush brush, Point position, RectangleAlign rectangleAlignment = RectangleAlign.TopLeft, TextAlign textAlignment = TextAlign.Left, bool drawEdge = false)
        {
            if (position.X >= 0
                && position.X <= panel_CameraView.DisplayRectangle.Width
                && position.Y >= 0
                && position.Y <= panel_CameraView.DisplayRectangle.Height)
            {
                StringAlignment textHorizontalAlignment = StringAlignment.Near; // Relative to top left corner
                switch (textAlignment)
                {
                    case TextAlign.Center:
                        textHorizontalAlignment = StringAlignment.Center;
                        break;
                    case TextAlign.Right:
                        textHorizontalAlignment = StringAlignment.Far;
                        break;
                    default: // TextAlign.Left
                        break;
                }
                StringFormat textFormat = new StringFormat()
                {
                    Alignment = textHorizontalAlignment,
                    LineAlignment = StringAlignment.Center, // Relative to top left corner
                    FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip
                };

                Size textSize = TextSize(text, font);
                switch (rectangleAlignment)
                {
                    case RectangleAlign.TopCenter:
                        position.X -= textSize.Width / 2;
                        break;
                    case RectangleAlign.TopRight:
                        position.X -= textSize.Width + 1;
                        break;
                    case RectangleAlign.MiddleLeft:
                        position.Y -= textSize.Height / 2;
                        break;
                    case RectangleAlign.MiddleCenter:
                        position.X -= textSize.Width / 2;
                        position.Y -= textSize.Height / 2;
                        break;
                    case RectangleAlign.MiddleRight:
                        position.X -= textSize.Width + 1;
                        position.Y -= textSize.Height / 2;
                        break;
                    case RectangleAlign.BottomLeft:
                        position.Y -= textSize.Height + 1;
                        break;
                    case RectangleAlign.BottomCenter:
                        position.X -= textSize.Width / 2;
                        position.Y -= textSize.Height + 1;
                        break;
                    case RectangleAlign.BottomRight:
                        position.X -= textSize.Width + 1;
                        position.Y -= textSize.Height + 1;
                        break;
                    default: // RectangleAlign.TopLeft
                        break;
                }
                Rectangle textRectangle = new Rectangle()
                {
                    Size = textSize,
                    Location = position
                };

                panelGraphics.DrawString(text, font, brush, textRectangle, textFormat);

                if (drawEdge) panelGraphics.DrawRectangle(new Pen(brush.Color, 1), textRectangle);

                return textRectangle;
            }
            else
            {
                return new Rectangle();
            }
        }

        private Size TextSize(string text, Font font)
        {
            SizeF size = panelGraphics.MeasureString(text.Split('\n').OrderByDescending(s => s.Length).FirstOrDefault(), font);
            return new Size()
            {
                Width = (int)Math.Ceiling(size.Width) + 4,
                Height = (int)Math.Ceiling(size.Height * (text.Count(c => c == '\n') + 1))
            };
        }
    }
}
