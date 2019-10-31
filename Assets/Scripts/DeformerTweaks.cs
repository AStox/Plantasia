using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tweaks{
    public class DeformerTweaks {
      
        /*
            Needs a random value method () and a method that accepts
         */
        
        public void RandomTweak(Deform.Deformer deformer) {
            /* 
                Tweak(deformer, Random.float(0,1))
             */
        }

        public void Tweak (Deform.Deformer deformerObj, float amount){
            amount = Mathf.Min(Mathf.Max(amount, -1f), 1f);
            switch(deformerObj){
                case Deform.BendDeformer def:
                    BendTweaks(def, amount);
                    break;
                case Deform.BlendDeformer def:
                    BlendTweaks(def, amount);
                    break;
                case Deform.BulgeDeformer def:
                    BulgeTweaks(def, amount);
                    break;
                case Deform.CubifyDeformer def:
                    CubifyTweaks(def, amount);
                    break;
                case Deform.CurveDisplaceDeformer def:
                    CurveDisplaceTweaks(def, amount);
                    break;
                case Deform.CurveScaleDeformer def:
                    CurveScaleTweaks(def, amount);
                    break;
                case Deform.CylindrifyDeformer def:
                    CylindrifyTweaks(def, amount);
                    break;
                case Deform.InflateDeformer def:
                    InflateTweaks(def, amount);
                    break;
                case Deform.LatheDisplaceDeformer def:
                    LatheDisplaceTweaks(def, amount);
                    break;
                case Deform.MagnetDeformer def:
                    MagnetTweaks(def, amount);
                    break;
                case Deform.MeltDeformer def:
                    MeltTweaks(def, amount);
                    break;
                case Deform.RadialCurveDeformer def:
                    RadialCurveTweaks(def, amount);
                    break;
                case Deform.RadialSkewDeformer def:
                    RadialSkewTweaks(def, amount);
                    break;
                case Deform.RippleDeformer def:
                    RippleTweaks(def, amount);
                    break;
                case Deform.ScaleDeformer def:
                    ScaleTweaks(def, amount);
                    break;
                case Deform.SineDeformer def:
                    SineTweaks(def, amount);
                    break;
                case Deform.SkewDeformer def:
                    SkewTweaks(def, amount);
                    break;
                case Deform.SpherifyDeformer def:
                    SpherifyTweaks(def, amount);
                    break;
                case Deform.SquashAndStretchDeformer def:
                    SquashAndStretchTweaks(def, amount);
                    break;
                case Deform.StarDeformer def:
                    StarTweaks(def, amount);
                    break;
                case Deform.TaperDeformer def:
                    TaperTweaks(def, amount);
                    break;
                case Deform.TextureDisplaceDeformer def:
                    TextureDisplaceTweaks(def, amount);
                    break;
                case Deform.TransformDeformer def:
                    TransformTweaks(def, amount);
                    break;
                case Deform.TransformOffsetDeformer def:
                    TransformOffsetTweaks(def, amount);
                    break;
                case Deform.TwirlDeformer def:
                    TwirlTweaks(def, amount);
                    break;
                case Deform.TwistDeformer def:
                    TwistTweaks(def, amount);
                    break;
                case Deform.UVOffsetDeformer def:
                    UVOffsetTweaks(def, amount);
                    break;
                case Deform.UVScaleDeformer def:
                    UVScaleTweaks(def, amount);
                    break;
                case Deform.WaveDeformer def:
                    WaveTweaks(def, amount);
                    break;
            }
        }



void BendTweaks(Deform.BendDeformer def, float amount) {

}

void BlendTweaks(Deform.BlendDeformer def, float amount) {

}

void BulgeTweaks(Deform.BulgeDeformer def, float amount) {

}

void CubifyTweaks(Deform.CubifyDeformer def, float amount) {

}

void CurveDisplaceTweaks(Deform.CurveDisplaceDeformer def, float amount) {

}

void CurveScaleTweaks(Deform.CurveScaleDeformer def, float amount) {

}

void CylindrifyTweaks(Deform.CylindrifyDeformer def, float amount) {

}

void InflateTweaks(Deform.InflateDeformer def, float amount) {

}

void LatheDisplaceTweaks(Deform.LatheDisplaceDeformer def, float amount) {

}

void MagnetTweaks(Deform.MagnetDeformer def, float amount) {

}

void MeltTweaks(Deform.MeltDeformer def, float amount) {

}

void RadialCurveTweaks(Deform.RadialCurveDeformer def, float amount) {

}

void RadialSkewTweaks(Deform.RadialSkewDeformer def, float amount) {

}

void RippleTweaks(Deform.RippleDeformer def, float amount) {

}

void ScaleTweaks(Deform.ScaleDeformer def, float amount) {

}

void SineTweaks(Deform.SineDeformer def, float amount) {

}

void SkewTweaks(Deform.SkewDeformer def, float amount) {

}

void SpherifyTweaks(Deform.SpherifyDeformer def, float amount) {

}

void SquashAndStretchTweaks(Deform.SquashAndStretchDeformer def, float amount) {

}

void StarTweaks(Deform.StarDeformer def, float amount) {

}

void TaperTweaks(Deform.TaperDeformer def, float amount) {

}

void TextureDisplaceTweaks(Deform.TextureDisplaceDeformer def, float amount) {

}

void TransformTweaks(Deform.TransformDeformer def, float amount) {

}

void TransformOffsetTweaks(Deform.TransformOffsetDeformer def, float amount) {

}

void TwirlTweaks(Deform.TwirlDeformer def, float amount) {
    def.Angle = 50;
}

void TwistTweaks(Deform.TwistDeformer def, float amount) {
    
}

void UVOffsetTweaks(Deform.UVOffsetDeformer def, float amount) {

}

void UVScaleTweaks(Deform.UVScaleDeformer def, float amount) {

}

void WaveTweaks(Deform.WaveDeformer def, float amount) {

}



















    }
}