using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLoop {
    private AudioSource source;
    private float startBoundary;
    private float introBoundary;
    private float loopBoundary;
    //set to play a clip once
    public bool playOnce = false;

    //play from start for intro
    public IntroLoop(AudioSource source, float introBoundary, float loopBoundary) {
        this.source = source;
        this.startBoundary = 0;
        this.introBoundary = introBoundary;
        this.loopBoundary = loopBoundary;
    }
    //play from start for intro or just loop
    public IntroLoop(AudioSource source, float introBoundary, float loopBoundary, bool playIntro) {
        this.source = source;
        this.startBoundary = playIntro?0:introBoundary;
        this.introBoundary = introBoundary;
        this.loopBoundary = loopBoundary;
    }
    //play from startBoundary for intro, then loop
    public IntroLoop(AudioSource source, float startBoundary, float introBoundary, float loopBoundary) {
        this.source = source;
        this.startBoundary = startBoundary;
        this.introBoundary = introBoundary;
        this.loopBoundary = loopBoundary;
    }
    //call to start
    public void start() { this.source.time = this.startBoundary; this.source.Play(); }
    //call every frame
    public void checkTime() {
        //Debug.Log(this.source.time);
        if (this.source.time >= this.loopBoundary) {
            if (!this.playOnce) { this.source.time = introBoundary; }
        } 
    }
    //call to stop
    public void stop() { this.source.Stop(); }  
}
