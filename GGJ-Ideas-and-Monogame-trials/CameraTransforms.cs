using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GGJ_Ideas_and_Monogame_trials
{
    class CameraTransforms
    {
        private Vector3 cameraFoci = new Vector3(0, 0, 0);
        private float cameraRotation = 0;
        private float cameraZ;
        private int viewportWidth;
        private int viewportHeight;

        private Matrix worldMatrix;
        private Matrix viewMatrix;
        private Matrix projectionMatrix;

        public CameraTransforms(int viewportWidth, int viewportHeight, float initialCameraZ) {
            this.viewportWidth = viewportWidth;
            this.viewportHeight = viewportHeight;
            this.cameraZ = initialCameraZ;
            CalculateWorldMatrix();
            CalculateViewMatrix();
            CalculateProjectionMatrix();
        }

        // -- World matrix and related updates
        //
        private void CalculateWorldMatrix()
        {
            // FIXME - something is missing
            // worldMatrix = Matrix.CreateTranslation(cameraFoci);
            worldMatrix = Matrix.CreateRotationY(cameraRotation);
        }
        public void SetCameraOrbitDegrees(float cameraRotationDegrees)
        {
            this.cameraRotation = MathHelper.ToRadians(cameraRotationDegrees);
            CalculateWorldMatrix();
        }
        public void IncrementCameraOrbitDegrees(float cameraRotationDegrees)
        {
            this.cameraRotation += MathHelper.ToRadians(cameraRotationDegrees);
            CalculateWorldMatrix();
        }

        //
        // -- View matrix and related updates
        private void CalculateViewMatrix()
        {
            // Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, cameraZ), new Vector3(0, 0, 0), Vector3.UnitY);
        }
        public void ZoomIn()
        {
            cameraZ *= 0.9f;
            CalculateViewMatrix();
        }
        public void ZoomOut()
        {
            cameraZ *= 1.1f;
            CalculateViewMatrix();
        }

        //
        // -- Projection matrix and related updates
        private const float FOV = MathF.PI / 4;
        private const float NEAR_CLIP = 0.1f;
        private const float FAR_CLIP = 100f;

        private void CalculateProjectionMatrix()
        {
            float viewPortAspectRatio = (float) viewportWidth / viewportHeight;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(FOV, viewPortAspectRatio, NEAR_CLIP, FAR_CLIP);
        }
        public void UpdateViewportDimensions(int viewportWidth, int viewportHeight)
        {
            if (this.viewportWidth != viewportWidth || this.viewportHeight != viewportHeight)
            {
                this.viewportWidth = viewportWidth;
                this.viewportHeight = viewportHeight;
                CalculateProjectionMatrix();
            }
        }

        // -- get the world,view and projection camera transforms
        public Matrix GetWorldMatrix()
        {
            return worldMatrix;
        }
        public Matrix GetViewMatrix()
        {
            return viewMatrix;
        }
        public Matrix GetProjectionMatrix()
        {
            return projectionMatrix;
        }
    }
}