using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CocosSharp;

namespace KeepShooting.Models
{
    public class BrokenMover
    {
        CCSprite _image;
        public CCSprite Image { get => _image; }
        CCLabel _brokenNumberLabel;
        int _brokenNumber;

        public int BrokenNumber { get => _brokenNumber; }

        public CCNode[] Nodes { get => new CCNode[] { _image, _brokenNumberLabel }; }

        public MoverType Type { get; }

        public BrokenMover(CCPoint position, string image, MoverType type)
        {
            _image = new CCSprite(image);
            _image.ScaleX = 0.5f;
            _image.ScaleY = 0.5f;
            Type = type;
            _brokenNumber = 0;
            _brokenNumberLabel = new CCLabel($"×{_brokenNumber}", "Arial", 30);
            _brokenNumberLabel.Color = CCColor3B.White;
            _brokenNumberLabel.AnchorPoint = new CCPoint(0, 1.0f);
            _image.AnchorPoint = new CCPoint(0, 1.0f);
            _image.Position = position;
            _brokenNumberLabel.Position = new CCPoint(position.X + 30, position.Y);

            _image.ZOrder = 0;
            _brokenNumberLabel.ZOrder = 0;
        }

        public void Increment()
        {
            _brokenNumber += 1;
            _brokenNumberLabel.Text = $"×{_brokenNumber}";
        }



    }
}
