package scxmlgen.Modalities;

import scxmlgen.interfaces.IModality;

/**
 *
 * @author nunof
 */
public enum SecondMod implements IModality{
    CLOSE_TAB("[0][Close_tab]",1500),
    OPEN_HISTORIC("[1][Historic]",1500),
    MAXIMIZE("[2][Maximize]",1500),
    MINIMIZE("[3][Minimize]",1500),
    OPEN_TAB("[4][Open_Tab]",1500),
    SCROLL_DOWN("[5][Scroll_Down]",1500),
    SCROLL_UP("[6][Scroll_Up]",1500),
    ZOOM_IN("[6][Zoom_In]",1500),
    ZOOM_OUT("[6][Zoom_Out]",1500),
    ;
    
    private String event;
    private int timeout;


    SecondMod(String m, int time) {
        event=m;
        timeout=time;
    }

    @Override
    public int getTimeOut() {
        return timeout;
    }

    @Override
    public String getEventName() {
        //return getModalityName()+"."+event;
        return event;
    }

    @Override
    public String getEvName() {
        return getModalityName().toLowerCase()+event.toLowerCase();
    }
    
}
