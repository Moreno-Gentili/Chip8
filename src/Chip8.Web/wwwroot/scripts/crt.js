import * as THREE from 'three';
import { EffectComposer } from 'three/addons/postprocessing/EffectComposer';
import { RenderPass } from 'three/addons/postprocessing/RenderPass';
import { ShaderPass } from 'three/addons/postprocessing/ShaderPass';

window.clearCanvas = function() {
    const canvas = document.querySelector('#lcd canvas');
    const ctx = canvas.getContext('2d');
    ctx.fillRect(0, 0, canvas.width, canvas.height);
};

window.renderCrt = function() {

    const scene = new THREE.Scene();
    const renderer = new THREE.WebGLRenderer({ alpha: true });

    const canvas = document.querySelector('#lcd canvas');
    const geometry = new THREE.PlaneGeometry(2.3, 1);
    const canvasTexture = new THREE.CanvasTexture(canvas);
    const material = new THREE.MeshStandardMaterial({
        color: new THREE.Color(0, 255, 0),
        map: canvasTexture,
        transparent: true,
        emissiveIntensity: 0.0055,
        emissive: new THREE.Color(0, 255, 0),
        emissiveMap: canvasTexture
    });

    const cube = new THREE.Mesh(geometry, material);
    scene.add(cube);

    // Create camera
    // const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
    const camera = new THREE.PerspectiveCamera(100, window.innerWidth / window.innerHeight, 1, 1000000);
    camera.position.z = 1.5;

    // Create effect composer
    const composer = new EffectComposer(renderer);
    composer.addPass(new RenderPass(scene, camera));

    // Add distortion effect to effect composer
    var effect = new ShaderPass(getDistortionShaderDefinition());
    composer.addPass(effect);
    effect.renderToScreen = true;

    // Setup distortion effect
    var horizontalFOV = 85;
    var strength = 0.85;
    var cylindricalRatio = 0.9;
    var height = Math.tan(THREE.MathUtils.degToRad(horizontalFOV) / 2) / camera.aspect;

    camera.fov = Math.atan(height) * 2 * 180 / 3.1415926535;
    camera.zoom = 1.12;
    camera.updateProjectionMatrix();

    effect.uniforms["strength"].value = strength;
    effect.uniforms["height"].value = height;
    effect.uniforms["aspectRatio"].value = camera.aspect;
    effect.uniforms["cylindricalRatio"].value = cylindricalRatio;

    renderer.setSize(640, 320);
    document.getElementById('crt').appendChild(renderer.domElement);

    animate.bind(this, () => {
        material.map.needsUpdate = true;
        composer.render(scene, camera);
    })();
}

// https://stackoverflow.com/questions/13360625/three-js-fisheye-effect
function getDistortionShaderDefinition() {
    return {

        uniforms: {
            "tDiffuse": { type: "t", value: null },
            "strength": { type: "f", value: 0 },
            "height": { type: "f", value: 1 },
            "aspectRatio": { type: "f", value: 1 },
            "cylindricalRatio": { type: "f", value: 1 }
        },

        vertexShader: [
            "uniform float strength;",          // s: 0 = perspective, 1 = stereographic
            "uniform float height;",            // h: tan(verticalFOVInRadians / 2)
            "uniform float aspectRatio;",       // a: screenWidth / screenHeight
            "uniform float cylindricalRatio;",  // c: cylindrical distortion ratio. 1 = spherical

            "varying vec3 vUV;",                // output to interpolate over screen
            "varying vec2 vUVDot;",             // output to interpolate over screen

            "void main() {",
            "gl_Position = projectionMatrix * (modelViewMatrix * vec4(position, 1.0));",

            "float scaledHeight = strength * height;",
            "float cylAspectRatio = aspectRatio * cylindricalRatio;",
            "float aspectDiagSq = aspectRatio * aspectRatio + 1.0;",
            "float diagSq = scaledHeight * scaledHeight * aspectDiagSq;",
            "vec2 signedUV = (2.0 * uv + vec2(-1.0, -1.0));",

            "float z = 0.5 * sqrt(diagSq + 1.0) + 0.5;",
            "float ny = (z - 1.0) / (cylAspectRatio * cylAspectRatio + 1.0);",

            "vUVDot = sqrt(ny) * vec2(cylAspectRatio, 1.0) * signedUV;",
            "vUV = vec3(0.5, 0.5, 1.0) * z + vec3(-0.5, -0.5, 0.0);",
            "vUV.xy += uv;",
            "}"
        ].join("\n"),

        fragmentShader: [
            "uniform sampler2D tDiffuse;",      // sampler of rendered scene?s render target
            "varying vec3 vUV;",                // interpolated vertex output data
            "varying vec2 vUVDot;",             // interpolated vertex output data

            "void main() {",
            "vec3 uv = dot(vUVDot, vUVDot) * vec3(-0.5, -0.5, -1.0) + vUV;",
            "gl_FragColor = texture2DProj(tDiffuse, uv);",
            "}"
        ].join("\n")

    };
}

function animate(render) {
    requestAnimationFrame(animate.bind(this, render));
    render();
}