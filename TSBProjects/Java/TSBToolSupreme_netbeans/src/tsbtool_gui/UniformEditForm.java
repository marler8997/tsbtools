/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package tsbtool_gui;

import java.awt.Color;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.swing.ComboBoxModel;
import javax.swing.DefaultComboBoxModel;
import javax.swing.ImageIcon;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import tsbtoolsupreme.InputParser;

/**
 *
 * @author Chris
 */
public class UniformEditForm
        extends javax.swing.JDialog {

    /**
     * Creates new form UniformEditForm
     */
    public UniformEditForm(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        mHomeUniformBufferdImage = MainGUI.GetImage("/tsbtool_gui/icons/HomePlayer.png", mColorForm2);
        mAwayUniformBufferdImage = MainGUI.GetImage("/tsbtool_gui/icons/HomePlayer.png", mColorForm2);
        mDivChampBufferdImage = MainGUI.GetImage("/tsbtool_gui/icons/DivChamp.PNG", mColorForm2);
        mConfChampBufferdImage = MainGUI.GetImage("/tsbtool_gui/icons/NFC_Champ.PNG", mColorForm2);

        initComponents();
        
        try{
            setIconImage(MainGUI.GetImage("/tsbtool_gui/icons/49ers.PNG", this));
        }
        catch(Exception e){
        }
    }

    private BufferedImage GetBufferedImage(String name) {
        BufferedImage ret = null;
        if (name.equals("HomePlayer")) {
            ret = mHomeUniformBufferdImage;
        } else if (name.equals("AwayPlayer")) {
            ret = mAwayUniformBufferdImage;
        } else if (name.equals("DivChamp")) {
            ret = mDivChampBufferdImage;
        } else if (name.equals("ConfChamp")) {
            ret = mConfChampBufferdImage;
        }

        return ret;
    }
    private BufferedImage mHomeUniformBufferdImage;
    private BufferedImage mAwayUniformBufferdImage;
    private BufferedImage mDivChampBufferdImage;
    private BufferedImage mConfChampBufferdImage;
    private final int Pants = 0;
    private final int Jersey = 1;
    private ColorForm mColorForm2 = new ColorForm(null, true);
    private boolean mCanceled = false;

    public boolean getCanceled() {
        return mCanceled;
    }
    private String m_Data = "";

    /// <summary>
    /// The text data to work on and retrieve.
    /// </summary>
    public String getData() {
        return m_Data;
    }

    public void setData(String value) {
        m_Data = value;
        if (m_Data != null && m_Data.length() > 0) {
            SetupTeams();
        }
    }

    private void SetupTeams() {
        Pattern teamRegex = Pattern.compile("TEAM\\s*=\\s*([a-z0-9]+)");
        Matcher mc = teamRegex.matcher(m_Data);

        DefaultComboBoxModel model = new DefaultComboBoxModel();
        String team = "";
        while (mc.find()) {
            team = mc.group(1);
            model.addElement(team);
        }
        mTeamsComboBox.setModel(model);
        if (model.getSize() > 0) {
            mTeamsComboBox.setSelectedIndex(0);
        }
    }

    /// <summary>
    /// Get and set the current team.
    /// </summary>
    public String getCurrentTeam() {
        return this.mTeamsComboBox.getSelectedItem().toString();
    }

    public void setCurrentTeam(String value) {
        int index = MainGUI.ComboBoxIndexOf(mTeamsComboBox, value);
        if (index > -1) {
            mTeamsComboBox.setSelectedIndex(index);
            SetCurrentTeamColors();
        }
    }

    /// <summary>
    /// Updates the GUI with the current team colors.
    /// </summary>
    private void SetCurrentTeamColors() {
        if (mTeamsComboBox.getSelectedItem() != null) {
            String colorData = GetColorString(getCurrentTeam());
            if (colorData != null) {
                SetFormColorData(colorData);
            }
        }
    }

    /// <summary>
    /// Sets up the form with the data in 'colorData'.
    /// </summary>
    /// <param name="colorData"></param>
    private void SetFormColorData(String colorData) {
        String homeUniform = InputParser.GetHomeUniformColorString(colorData);
        String awayUniform = InputParser.GetAwayUniformColorString(colorData);
        String confChamp = InputParser.GetConfChampColorString(colorData);
        String divChamp = InputParser.GetDivChampColorString(colorData);
        String uniformUsage = InputParser.GetUniformUsageString(colorData);

        if (homeUniform != null && homeUniform.length() > 5) {
            setHomePantsColorString(homeUniform.substring(0, 2));
            setSkin1ColorString(homeUniform.substring(2, 4));
            setHomeJerseyColorString(homeUniform.substring(4, 6));
        }
        if (awayUniform != null && awayUniform.length() > 5) {
            setAwayPantsColorString(awayUniform.substring(0, 2));
            setSkin2ColorString(awayUniform.substring(2, 4));
            setAwayJerseyColorString(awayUniform.substring(4, 6));
        }
        if (confChamp != null && confChamp.length() > 7) {
//            mDivChampGroupBox.Enabled = true;
            setConfrenceChampUniform1ColorString(confChamp.substring(0, 2));
            setConfrenceChampUniform2ColorString(confChamp.substring(2, 4));
            setConfrenceChampUniform3ColorString(confChamp.substring(4, 6));
            setConfrenceChampHelmetColorString(confChamp.substring(6, 8));
        }
//         else
//         {
//            mDivChampGroupBox.Enabled = false;
//         }
        if (divChamp != null && divChamp.length() > 9) {
//            mDivChampGroupBox.Enabled = true;
            setDivisionChampUniform1ColorString(divChamp.substring(0, 2));
            setDivisionChampUniform2ColorString(divChamp.substring(2, 4));
            setDivisionChampUniform3ColorString(divChamp.substring(4, 6));
            setDivisionChampHelmet1ColorString(divChamp.substring(6, 8));
            setDivisionChampHelmet2ColorString(divChamp.substring(8, 10));
        }
//         else
//         {
//            mDivChampGroupBox.Enabled = false;
//         }
        if (uniformUsage != null && uniformUsage.length() > 7) {
            UnifromUsageString = uniformUsage;
        }
    }

    /// <summary>
    /// Gets a color 'line' from m_Data from 'team' playing 'position'.
    /// </summary>
    /// <param name="team"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    private String GetColorString(String team) {
        String ret = "";
        String pattern = "TEAM\\s*=\\s*" + team;
        Pattern findTeamRegex = Pattern.compile(pattern);
        Matcher m = findTeamRegex.matcher(m_Data);
        if (m.find()) {
            int teamIndex = m.start();
            if (teamIndex == -1) {
                return null;
            }
            int lineStart = m_Data.indexOf("COLORS", teamIndex);
            int lineEnd = -1;
            if (lineStart > 0) {
                lineEnd = m_Data.indexOf("\n", lineStart + 3);
            }
            if (lineStart > -1 && lineEnd > -1) {
                ret = m_Data.substring(lineStart, lineEnd);
            }
        }
        return ret;
    }

    private void SetChampColors(JLabel lab) {
        ColorForm form = new ColorForm(null, true);
        form.setCurrentColorString(lab.getText());
        form.setTitle("Choose Color");

        form.setVisible(true);
        if (!form.getCanceled()) {
            if (lab.equals(mConfChampUniform1Label)) {
                setConfrenceChampUniform1ColorString(form.getCurrentColorString());
            } else if (lab.equals(mConfChampUniform2Label)) {
                setConfrenceChampUniform2ColorString(form.getCurrentColorString());
            } else if (lab.equals(mConfChampUniform3Label)) {
                setConfrenceChampUniform3ColorString(form.getCurrentColorString());
            } else if (lab.equals(mConfChampHelmetLabel)) {
                setConfrenceChampHelmetColorString(form.getCurrentColorString());
            } else if (lab.equals(mDivChampUniform1Label)) {
                setDivisionChampUniform1ColorString(form.getCurrentColorString());
            } else if (lab.equals(mDivChampUniform2Label)) {
                setDivisionChampUniform2ColorString(form.getCurrentColorString());
            } else if (lab.equals(mDivChampUniform3Label)) {
                setDivisionChampUniform3ColorString(form.getCurrentColorString());
            } else if (lab.equals(mDivChampHelmet1Label)) {
                setDivisionChampHelmet1ColorString(form.getCurrentColorString());
            } else if (lab.equals(mDivChampHelmet2Label)) {
                setDivisionChampHelmet2ColorString(form.getCurrentColorString());
            }
            ReplaceColorData();
        }

        form.dispose();
    }

    private void ReplaceColorData() {
        String oldData = GetColorString(getCurrentTeam());
        String newData = GetCurrentTeamColorData_UI();
        if (oldData != null) {
            ReplaceColorData(getCurrentTeam(), oldData, newData);
        }
    }

    private void ReplaceColorData(String team, String oldData, String newData) {
        int nextTeamIndex = -1;
        int currentTeamIndex = -1;
        String nextTeam = null;

        Pattern findTeamRegex = Pattern.compile("TEAM\\s*=\\s*" + team);

        Matcher m = findTeamRegex.matcher(m_Data);
        if (!m.find()) {
            return;
        }

        currentTeamIndex = m.start();

        int test = MainGUI.ComboBoxIndexOf(mTeamsComboBox, team);

        if (test != mTeamsComboBox.getModel().getSize() - 1) {
            nextTeam = String.format("TEAM\\s*=\\s*%s", mTeamsComboBox.getModel().getElementAt(test + 1));
            Pattern nextTeamRegex = Pattern.compile(nextTeam);
            Matcher nt = nextTeamRegex.matcher(m_Data);
            if (nt.find()) {
                nextTeamIndex = nt.start();
            }
            //nextTeamIndex = m_Data.IndexOf(nextTeam);
        }
        if (nextTeamIndex < 0) {
            nextTeamIndex = m_Data.length();
        }


        int dataIndex = m_Data.indexOf(oldData, currentTeamIndex);
        if (dataIndex > -1) {
            int endLine = m_Data.indexOf('\n', dataIndex);
            String start = m_Data.substring(0, dataIndex);
            String last = m_Data.substring(endLine);

            StringBuilder tmp = new StringBuilder(m_Data.length() + 200);
            tmp.append(start);
            tmp.append(newData);
            tmp.append(last);

            m_Data = tmp.toString();
            //m_Data = start + newPlayer + last;
        } else {
            String error = String.format(
                    "An error occured looking up team\n"
                    + "     '%s'\n"
                    + "Please verify that this teams's color attributes are correct.", oldData);
            JOptionPane.showMessageDialog(this, "ERROR", error, JOptionPane.ERROR_MESSAGE);
        }
    }

    /// <summary>
    /// Gets the color string represented by the current state of the UI.
    /// </summary>
    /// <returns></returns>
    private String GetCurrentTeamColorData_UI() {
        //COLORS Home=0x3038, Away=0x3038, Skin=0x060f, DivChamp=0x0111012515, ConfChamp=0x11012501
        String ret = String.format(
                "COLORS Uniform1=0x%s%s%s, Uniform2=0x%s%s%s, "
                + "DivChamp=0x%s%s%s%s%s, ConfChamp=0x%s%s%s%s,UniformUsage=0x%s",
                getHomePantsColorString(),
                getSkin1ColorString(),
                getHomeJerseyColorString(),
                getAwayPantsColorString(),
                getSkin2ColorString(),
                getAwayJerseyColorString(),
                getDivisionChampUniform1ColorString(),
                getDivisionChampUniform2ColorString(),
                getDivisionChampUniform3ColorString(),
                getDivisionChampHelmet1ColorString(),
                getDivisionChampHelmet2ColorString(),
                getConfrenceChampUniform1ColorString(),
                getConfrenceChampUniform2ColorString(),
                getConfrenceChampUniform3ColorString(),
                getConfrenceChampHelmetColorString(),
                UnifromUsageString);
        return ret;
    }

    private void MakeLabelReadable(String colorString, JLabel lab) {
        if (colorString.startsWith("20") || colorString.startsWith("3")) {
            lab.setForeground(Color.BLACK);
        } else {
            lab.setForeground(Color.WHITE);
        }
        if (colorString.endsWith("F") || colorString.endsWith("E")) {
            lab.setForeground(Color.WHITE);
        }
    }

    private void SetBufferedImageColor(BufferedImage bi, Color c, int[] locations) {
        int theColor = c.getRGB();
        for (int i = 0; i < locations.length; i += 2) {
            bi.setRGB(locations[i], locations[i + 1], theColor);
        }
    }

    private void SetJerseyColor(BufferedImage bmp, Color jersey) {
        int x = 0;
        int y = 0;
        int jColor = jersey.getRGB();
        for (int i = 0; i < UniformEditData2.mJerseyColorPositions.length; i = i + 2) {
            x = UniformEditData2.mJerseyColorPositions[i];
            y = UniformEditData2.mJerseyColorPositions[i + 1];
            bmp.setRGB(x, y, jColor);
        }
    }

    private void SetPantsColor(BufferedImage bmp, Color pants) {
        int pColor = pants.getRGB();
        for (int i = 0; i < UniformEditData2.mPantsColorPositions.length; i += 2) {
            bmp.setRGB(UniformEditData2.mPantsColorPositions[i], UniformEditData2.mPantsColorPositions[i + 1], pColor);
        }
    }

    public String getConfrenceChampUniform1ColorString() {
        return mConfChampUniform1Label.getText();
    }

    public void setConfrenceChampUniform1ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mConfChampUniform1Label.setText(value);
//                    mConfrenceChampUniform1ColorString = value; 
            mConfChampUniform1Label.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mConfChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData2.mConfChampUniform1);
            mConfChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getConfrenceChampUniform1ColorString(), mConfChampUniform1Label);
        }
    }

    public String getConfrenceChampUniform2ColorString() {
        return mConfChampUniform2Label.getText();
    }

    public void setConfrenceChampUniform2ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mConfChampUniform2Label.setText(value);
//                    mConfrenceChampUniform2ColorString = value; 
            mConfChampUniform2Label.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mConfChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData2.mConfChampUniform2);
            mConfChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getConfrenceChampUniform2ColorString(), mConfChampUniform2Label);
        }
    }

//        private String mConfrenceChampUniform3ColorString;
    public String getConfrenceChampUniform3ColorString() {
        return mConfChampUniform3Label.getText();
    }

    public void setConfrenceChampUniform3ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mConfChampUniform3Label.setText(value);
//                    mConfrenceChampUniform3ColorString = value; 
            mConfChampUniform3Label.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mConfChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData3.mConfChampUniform3);
            mConfChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getConfrenceChampUniform3ColorString(), mConfChampUniform3Label);
        }
    }

//        private String mConfrenceChampHelmetColorString;
    public String getConfrenceChampHelmetColorString() {
        return mConfChampHelmetLabel.getText();
    }

    public void setConfrenceChampHelmetColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mConfChampHelmetLabel.setText(value);
//                    mConfrenceChampHelmetColorString = value; 
            mConfChampHelmetLabel.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mConfChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData3.mConfChampHelmet);
            mConfChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getConfrenceChampHelmetColorString(), mConfChampHelmetLabel);
        }
    }

//        private String mDivisionChampUniform1ColorString;
    public String getDivisionChampUniform1ColorString() {
        return mDivChampUniform1Label.getText();
    }

    public void setDivisionChampUniform1ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mDivChampUniform1Label.setText(value);
//                    mDivisionChampUniform1ColorString = value; 
            mDivChampUniform1Label.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mDivChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData.mDivChampUniform1);
            mDivChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getDivisionChampUniform1ColorString(), mDivChampUniform1Label);
        }
    }
//        private String mDivisionChampUniform2ColorString;

    public String getDivisionChampUniform2ColorString() {
        return mDivChampUniform2Label.getText();
    }

    public void setDivisionChampUniform2ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mDivChampUniform2Label.setText(value);
//                    mDivisionChampUniform2ColorString = value; 
            mDivChampUniform2Label.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mDivChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData.mDivChampUniform2);
            mDivChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getDivisionChampUniform2ColorString(), mDivChampUniform2Label);
        }
    }
//        private String mDivisionChampUniform3ColorString;

    public String getDivisionChampUniform3ColorString() {
        return mDivChampUniform3Label.getText();
    }

    public void setDivisionChampUniform3ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mDivChampUniform3Label.setText(value);
//                    mDivisionChampUniform3ColorString = value; 
            mDivChampUniform3Label.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mDivChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData.mDivChampUniform3);
            mDivChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getDivisionChampUniform3ColorString(), mDivChampUniform3Label);
        }
    }
//        private String mDivisionChampHelmet1ColorString;

    public String getDivisionChampHelmet1ColorString() {
        return mDivChampHelmet1Label.getText();
    }

    public void setDivisionChampHelmet1ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mDivChampHelmet1Label.setText(value);
//                    mDivisionChampHelmet1ColorString = value; 
            mDivChampHelmet1Label.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mDivChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData2.mDivChampHelmet1);
            mDivChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getDivisionChampHelmet1ColorString(), mDivChampHelmet1Label);
        }
    }

//        private String mDivisionChampHelmet2ColorString;
    public String getDivisionChampHelmet2ColorString() {
        return mDivChampHelmet2Label.getText();
    }

    public void setDivisionChampHelmet2ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mDivChampHelmet2Label.setText(value);
//                    mDivisionChampHelmet2ColorString = value; 
            mDivChampHelmet2Label.setBackground(c);
            BufferedImage bmp = GetBufferedImage(mDivChampPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData2.mDivChampHelmet2);
            mDivChampPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getDivisionChampHelmet2ColorString(), mDivChampHelmet2Label);
        }
    }

    public String getHomeJerseyColorString() {
        return mHomeJerseyLabel.getText();
    }

    public void setHomeJerseyColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mHomeJerseyLabel.setText(value);
            mHomeJerseyLabel.setBackground(c);
            MakeLabelReadable(mHomeJerseyLabel.getText(), mHomeJerseyLabel);
            BufferedImage bmp = GetBufferedImage(mHomePictureBox.getName());
            SetJerseyColor(bmp, c);
            mHomePictureBox.setIcon(new ImageIcon(bmp));
        }
    }

    public String getHomePantsColorString() {
        return mHomePantsLabel.getText();
    }

    public void setHomePantsColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mHomePantsLabel.setText(value);
            mHomePantsLabel.setBackground(c);
            MakeLabelReadable(mHomePantsLabel.getText(), mHomePantsLabel);
            BufferedImage bmp = GetBufferedImage(mHomePictureBox.getName());
            SetPantsColor(bmp, c);
            mHomePictureBox.setIcon(new ImageIcon(bmp));
        }
    }

    public String getAwayJerseyColorString() {
        return mAwayJerseyLabel.getText();
    }

    public void setAwayJerseyColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mAwayJerseyLabel.setText(value);
            mAwayJerseyLabel.setBackground(c);
            MakeLabelReadable(mAwayJerseyLabel.getText(), mAwayJerseyLabel);
            BufferedImage bmp = GetBufferedImage(mAwayPictureBox.getName());
            SetJerseyColor(bmp, c);
            mAwayPictureBox.setIcon(new ImageIcon(bmp));
        }
    }

    public String getAwayPantsColorString() {
        return mAwayPantsLabel.getText();
    }

    public void setAwayPantsColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mAwayPantsLabel.setText(value);
            mAwayPantsLabel.setBackground(c);
            MakeLabelReadable(mAwayPantsLabel.getText(), mAwayPantsLabel);
            BufferedImage bmp = GetBufferedImage(mAwayPictureBox.getName());
            SetPantsColor(bmp, c);
            mAwayPictureBox.setIcon(new ImageIcon(bmp));
        }
    }

    public String getSkin1ColorString() {
        return mSkin1Label.getText();
    }

    public void setSkin1ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mSkin1Label.setText(value);
            mSkin1Label.setBackground(c);
            MakeLabelReadable(mSkin1Label.getText(), mSkin1Label);
            BufferedImage bmp = GetBufferedImage(mHomePictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData2.mSkinData);
            mHomePictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getSkin1ColorString(), mSkin1Label);
        }
    }

    public String getSkin2ColorString() {
        return mSkin2Label.getText();
    }

    public void setSkin2ColorString(String value) {
        Color c = mColorForm2.GetColor(value);
        if (c != null) {
            mSkin2Label.setText(value);
            mSkin2Label.setBackground(c);
            MakeLabelReadable(mSkin2Label.getText(), mSkin2Label);
            BufferedImage bmp = GetBufferedImage(mAwayPictureBox.getName());
            SetBufferedImageColor(bmp, c, UniformEditData2.mSkinData);
            mAwayPictureBox.setIcon(new ImageIcon(bmp));
            MakeLabelReadable(getSkin2ColorString(), mSkin2Label);
        }
    }
    public String UnifromUsageString = "";

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents()
    {

        jLabel1 = new javax.swing.JLabel();
        mTeamsComboBox = new javax.swing.JComboBox();
        mUniform1GroupBox = new javax.swing.JPanel();
        mHomePictureBox = new javax.swing.JLabel();
        mSkin1Button = new javax.swing.JButton();
        mSkin1Label = new javax.swing.JLabel();
        mHomeJerseyButton = new javax.swing.JButton();
        mHomeJerseyLabel = new javax.swing.JLabel();
        mHomePantsButton = new javax.swing.JButton();
        mHomePantsLabel = new javax.swing.JLabel();
        mUniform2GroupBox = new javax.swing.JPanel();
        mAwayPictureBox = new javax.swing.JLabel();
        mSkin2Button = new javax.swing.JButton();
        mSkin2Label = new javax.swing.JLabel();
        mAwayJerseyButton = new javax.swing.JButton();
        mAwayJerseyLabel = new javax.swing.JLabel();
        mAwayPantsButton = new javax.swing.JButton();
        mAwayPantsLabel = new javax.swing.JLabel();
        mDivChampGroupBox = new javax.swing.JPanel();
        mDivChampPictureBox = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        mDivChampUniform1Label = new javax.swing.JLabel();
        mDivChampUniform2Label = new javax.swing.JLabel();
        jLabel5 = new javax.swing.JLabel();
        mDivChampUniform3Label = new javax.swing.JLabel();
        jLabel7 = new javax.swing.JLabel();
        mDivChampHelmet1Label = new javax.swing.JLabel();
        jLabel9 = new javax.swing.JLabel();
        mDivChampHelmet2Label = new javax.swing.JLabel();
        jLabel11 = new javax.swing.JLabel();
        mConfChampGroupBox = new javax.swing.JPanel();
        mConfChampPictureBox = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        mConfChampUniform1Label = new javax.swing.JLabel();
        mConfChampUniform2Label = new javax.swing.JLabel();
        jLabel6 = new javax.swing.JLabel();
        mConfChampUniform3Label = new javax.swing.JLabel();
        jLabel8 = new javax.swing.JLabel();
        mConfChampHelmetLabel = new javax.swing.JLabel();
        jLabel10 = new javax.swing.JLabel();
        mOKButton = new javax.swing.JButton();
        mCancelButton = new javax.swing.JButton();
        mUniformUsageButton = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Edit Uniforms");
        setMaximumSize(new java.awt.Dimension(690, 600));
        setMinimumSize(new java.awt.Dimension(670, 500));
        setPreferredSize(new java.awt.Dimension(680, 501));
        getContentPane().setLayout(null);

        jLabel1.setText("Team");
        getContentPane().add(jLabel1);
        jLabel1.setBounds(20, 10, 32, 16);

        mTeamsComboBox.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Item 1", "Item 2", "Item 3", "Item 4" }));
        mTeamsComboBox.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                mTeamsComboBoxActionPerformed(evt);
            }
        });
        getContentPane().add(mTeamsComboBox);
        mTeamsComboBox.setBounds(70, 10, 120, 26);

        mUniform1GroupBox.setBorder(javax.swing.BorderFactory.createTitledBorder("Uniform 1"));
        mUniform1GroupBox.setLayout(null);

        mHomePictureBox.setIcon(new ImageIcon(mHomeUniformBufferdImage));
        mHomePictureBox.setMaximumSize(new java.awt.Dimension(60, 92));
        mHomePictureBox.setMinimumSize(new java.awt.Dimension(56, 88));
        mHomePictureBox.setName("HomePlayer"); // NOI18N
        mUniform1GroupBox.add(mHomePictureBox);
        mHomePictureBox.setBounds(10, 20, 60, 100);

        mSkin1Button.setText("Skin 1");
        mSkin1Button.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                skin1MouseClicked(evt);
            }
        });
        mUniform1GroupBox.add(mSkin1Button);
        mSkin1Button.setBounds(10, 180, 70, 28);

        mSkin1Label.setText("00");
        mSkin1Label.setOpaque(true);
        mUniform1GroupBox.add(mSkin1Label);
        mSkin1Label.setBounds(90, 180, 30, 30);

        mHomeJerseyButton.setText("Jersey");
        mHomeJerseyButton.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                jerseyButton_Clicked(evt);
            }
        });
        mUniform1GroupBox.add(mHomeJerseyButton);
        mHomeJerseyButton.setBounds(10, 120, 70, 28);

        mHomeJerseyLabel.setText("00");
        mHomeJerseyLabel.setOpaque(true);
        mUniform1GroupBox.add(mHomeJerseyLabel);
        mHomeJerseyLabel.setBounds(90, 120, 30, 30);

        mHomePantsButton.setText("Pants");
        mHomePantsButton.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                pantsButton_Click(evt);
            }
        });
        mUniform1GroupBox.add(mHomePantsButton);
        mHomePantsButton.setBounds(10, 150, 70, 28);

        mHomePantsLabel.setText("00");
        mHomePantsLabel.setOpaque(true);
        mUniform1GroupBox.add(mHomePantsLabel);
        mHomePantsLabel.setBounds(90, 150, 30, 30);

        getContentPane().add(mUniform1GroupBox);
        mUniform1GroupBox.setBounds(10, 50, 140, 230);

        mUniform2GroupBox.setBorder(javax.swing.BorderFactory.createTitledBorder("Uniform 2"));
        mUniform2GroupBox.setLayout(null);

        mAwayPictureBox.setIcon(new ImageIcon(mAwayUniformBufferdImage)
        );
        mAwayPictureBox.setMinimumSize(new java.awt.Dimension(56, 88));
        mAwayPictureBox.setName("AwayPlayer"); // NOI18N
        mUniform2GroupBox.add(mAwayPictureBox);
        mAwayPictureBox.setBounds(10, 20, 60, 100);

        mSkin2Button.setText("Skin 2");
        mSkin2Button.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                skin2MouseClicked(evt);
            }
        });
        mUniform2GroupBox.add(mSkin2Button);
        mSkin2Button.setBounds(10, 180, 70, 28);

        mSkin2Label.setText("00");
        mSkin2Label.setOpaque(true);
        mUniform2GroupBox.add(mSkin2Label);
        mSkin2Label.setBounds(90, 180, 30, 30);

        mAwayJerseyButton.setText("Jersey");
        mAwayJerseyButton.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                jerseyButton_Clicked(evt);
            }
        });
        mUniform2GroupBox.add(mAwayJerseyButton);
        mAwayJerseyButton.setBounds(10, 120, 70, 28);

        mAwayJerseyLabel.setText("00");
        mAwayJerseyLabel.setOpaque(true);
        mUniform2GroupBox.add(mAwayJerseyLabel);
        mAwayJerseyLabel.setBounds(90, 120, 30, 30);

        mAwayPantsButton.setText("Pants");
        mAwayPantsButton.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                pantsButton_Click(evt);
            }
        });
        mUniform2GroupBox.add(mAwayPantsButton);
        mAwayPantsButton.setBounds(10, 150, 70, 28);

        mAwayPantsLabel.setText("00");
        mAwayPantsLabel.setOpaque(true);
        mUniform2GroupBox.add(mAwayPantsLabel);
        mAwayPantsLabel.setBounds(90, 150, 30, 30);

        getContentPane().add(mUniform2GroupBox);
        mUniform2GroupBox.setBounds(150, 50, 140, 230);

        mDivChampGroupBox.setBorder(javax.swing.BorderFactory.createTitledBorder("Division Champ Colors"));
        mDivChampGroupBox.setLayout(null);

        mDivChampPictureBox.setIcon(new ImageIcon(mDivChampBufferdImage));
        mDivChampPictureBox.setName("DivChamp"); // NOI18N
        mDivChampGroupBox.add(mDivChampPictureBox);
        mDivChampPictureBox.setBounds(90, 20, 230, 140);

        jLabel2.setText("Uni 1");
        mDivChampGroupBox.add(jLabel2);
        jLabel2.setBounds(10, 30, 40, 16);

        mDivChampUniform1Label.setText("00");
        mDivChampUniform1Label.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mDivChampUniform1Label.setOpaque(true);
        mDivChampUniform1Label.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mDivChampGroupBox.add(mDivChampUniform1Label);
        mDivChampUniform1Label.setBounds(60, 30, 20, 18);

        mDivChampUniform2Label.setText("00");
        mDivChampUniform2Label.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mDivChampUniform2Label.setOpaque(true);
        mDivChampUniform2Label.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mDivChampGroupBox.add(mDivChampUniform2Label);
        mDivChampUniform2Label.setBounds(60, 50, 20, 18);

        jLabel5.setText("Uni 2");
        mDivChampGroupBox.add(jLabel5);
        jLabel5.setBounds(10, 50, 40, 16);

        mDivChampUniform3Label.setText("00");
        mDivChampUniform3Label.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mDivChampUniform3Label.setOpaque(true);
        mDivChampUniform3Label.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mDivChampGroupBox.add(mDivChampUniform3Label);
        mDivChampUniform3Label.setBounds(60, 70, 20, 18);

        jLabel7.setText("Uni 3");
        mDivChampGroupBox.add(jLabel7);
        jLabel7.setBounds(10, 70, 40, 16);

        mDivChampHelmet1Label.setText("00");
        mDivChampHelmet1Label.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mDivChampHelmet1Label.setOpaque(true);
        mDivChampHelmet1Label.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mDivChampGroupBox.add(mDivChampHelmet1Label);
        mDivChampHelmet1Label.setBounds(60, 120, 20, 18);

        jLabel9.setText("Hel 1");
        mDivChampGroupBox.add(jLabel9);
        jLabel9.setBounds(10, 120, 40, 16);

        mDivChampHelmet2Label.setText("00");
        mDivChampHelmet2Label.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mDivChampHelmet2Label.setOpaque(true);
        mDivChampHelmet2Label.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mDivChampGroupBox.add(mDivChampHelmet2Label);
        mDivChampHelmet2Label.setBounds(60, 140, 20, 18);

        jLabel11.setText("Hel 2");
        mDivChampGroupBox.add(jLabel11);
        jLabel11.setBounds(10, 140, 40, 16);

        getContentPane().add(mDivChampGroupBox);
        mDivChampGroupBox.setBounds(310, 10, 330, 200);

        mConfChampGroupBox.setBorder(javax.swing.BorderFactory.createTitledBorder("Conference Champ Colors"));
        mConfChampGroupBox.setLayout(null);

        mConfChampPictureBox.setIcon(new ImageIcon(mConfChampBufferdImage));
        mConfChampPictureBox.setName("ConfChamp"); // NOI18N
        mConfChampGroupBox.add(mConfChampPictureBox);
        mConfChampPictureBox.setBounds(120, 20, 200, 140);

        jLabel3.setText("Uni 1");
        mConfChampGroupBox.add(jLabel3);
        jLabel3.setBounds(10, 30, 40, 16);

        mConfChampUniform1Label.setText("00");
        mConfChampUniform1Label.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mConfChampUniform1Label.setOpaque(true);
        mConfChampUniform1Label.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mConfChampGroupBox.add(mConfChampUniform1Label);
        mConfChampUniform1Label.setBounds(70, 30, 20, 18);

        mConfChampUniform2Label.setText("00");
        mConfChampUniform2Label.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mConfChampUniform2Label.setOpaque(true);
        mConfChampUniform2Label.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mConfChampGroupBox.add(mConfChampUniform2Label);
        mConfChampUniform2Label.setBounds(70, 50, 20, 18);

        jLabel6.setText("Uni 2");
        mConfChampGroupBox.add(jLabel6);
        jLabel6.setBounds(10, 50, 40, 16);

        mConfChampUniform3Label.setText("00");
        mConfChampUniform3Label.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mConfChampUniform3Label.setOpaque(true);
        mConfChampUniform3Label.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mConfChampGroupBox.add(mConfChampUniform3Label);
        mConfChampUniform3Label.setBounds(70, 70, 20, 18);

        jLabel8.setText("Uni 3");
        mConfChampGroupBox.add(jLabel8);
        jLabel8.setBounds(10, 70, 40, 16);

        mConfChampHelmetLabel.setText("00");
        mConfChampHelmetLabel.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        mConfChampHelmetLabel.setOpaque(true);
        mConfChampHelmetLabel.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                uniformLabel_Click(evt);
            }
        });
        mConfChampGroupBox.add(mConfChampHelmetLabel);
        mConfChampHelmetLabel.setBounds(70, 120, 20, 18);

        jLabel10.setText("Hel 1");
        mConfChampGroupBox.add(jLabel10);
        jLabel10.setBounds(10, 120, 40, 16);

        getContentPane().add(mConfChampGroupBox);
        mConfChampGroupBox.setBounds(310, 220, 330, 200);

        mOKButton.setText("OK");
        mOKButton.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                mOKButtonActionPerformed(evt);
            }
        });
        getContentPane().add(mOKButton);
        mOKButton.setBounds(370, 430, 120, 28);

        mCancelButton.setText("Cancel");
        mCancelButton.addActionListener(new java.awt.event.ActionListener()
        {
            public void actionPerformed(java.awt.event.ActionEvent evt)
            {
                mCancelButtonActionPerformed(evt);
            }
        });
        getContentPane().add(mCancelButton);
        mCancelButton.setBounds(520, 430, 110, 28);

        mUniformUsageButton.setText("Edit Uniform Usage");
        mUniformUsageButton.addMouseListener(new java.awt.event.MouseAdapter()
        {
            public void mouseClicked(java.awt.event.MouseEvent evt)
            {
                mUniformUsageButtonMouseClicked(evt);
            }
        });
        getContentPane().add(mUniformUsageButton);
        mUniformUsageButton.setBounds(20, 310, 210, 100);

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void mCancelButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mCancelButtonActionPerformed
        mCanceled = true;
        setVisible(false);
    }//GEN-LAST:event_mCancelButtonActionPerformed

    private void mOKButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mOKButtonActionPerformed
        mCanceled = false;
        setVisible(false);
    }//GEN-LAST:event_mOKButtonActionPerformed

    private void uniformLabel_Click(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_uniformLabel_Click
        JLabel lab = (JLabel) evt.getSource();
        SetChampColors(lab);
    }//GEN-LAST:event_uniformLabel_Click

    private void jerseyButton_Clicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_jerseyButton_Clicked
        JLabel box = this.mHomePictureBox;
        JLabel lbl = this.mHomeJerseyLabel;
        if (evt.getSource() == this.mAwayJerseyButton) {
            box = this.mAwayPictureBox;
            lbl = this.mAwayJerseyLabel;
        }
        SetUniformColor("Select Jersey Color", box, lbl, Jersey);
    }//GEN-LAST:event_jerseyButton_Clicked

    private void pantsButton_Click(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_pantsButton_Click
        JLabel box = this.mHomePictureBox;
        JLabel lbl = this.mHomePantsLabel;
        if (evt.getSource() == this.mAwayPantsButton) {
            box = this.mAwayPictureBox;
            lbl = this.mAwayPantsLabel;
        }
        SetUniformColor("Select Pants Color", box, lbl, Pants);
    }//GEN-LAST:event_pantsButton_Click

    private void skin1MouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_skin1MouseClicked
        ColorForm form = new ColorForm(null, true);
        form.setCurrentColorString(mSkin1Label.getText());
        form.setVisible(true);
        if (!form.getCanceled()) {
            this.setSkin1ColorString(form.getCurrentColorString());
            ReplaceColorData();
        }
        form.dispose();
    }//GEN-LAST:event_skin1MouseClicked

    private void skin2MouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_skin2MouseClicked
        ColorForm form = new ColorForm(null, true);
        form.setCurrentColorString(mSkin2Label.getText());
        form.setVisible(true);
        if (!form.getCanceled()) {
            this.setSkin2ColorString(form.getCurrentColorString());
            ReplaceColorData();
        }
        form.dispose();
    }//GEN-LAST:event_skin2MouseClicked

    private void mTeamsComboBoxActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_mTeamsComboBoxActionPerformed
        SetCurrentTeamColors();
    }//GEN-LAST:event_mTeamsComboBoxActionPerformed

    private void mUniformUsageButtonMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_mUniformUsageButtonMouseClicked
        if (getData().indexOf("texans") > -1) {
            CXRomUniformUsageForm form = new CXRomUniformUsageForm(null, true);
            form.setStringValue(UnifromUsageString);
            form.setVisible(true);
            if (!form.getCanceled()) {
                UnifromUsageString = form.getStringValue();
                ReplaceColorData();
            }
            form.dispose();
        } else {
            HomeAwayUniformForm form = new HomeAwayUniformForm(null, true);
            form.setStringValue(UnifromUsageString);
            form.setVisible(true);
            if (!form.getCanceled()) {
                UnifromUsageString = form.getStringValue();
                ReplaceColorData();
            }
            form.dispose();
        }
    }//GEN-LAST:event_mUniformUsageButtonMouseClicked

    private void SetUniformColor(String formText, JLabel box, JLabel lbl, int choice /*Pants|Jersey*/) {
        ColorForm form = new ColorForm(null, true);
        form.setCurrentColorString(lbl.getText());
        form.setTitle(formText);
        form.setVisible(true);

        if (!form.getCanceled()) {
            //Bitmap bmp = new Bitmap(box.Image);
            BufferedImage bmp = GetBufferedImage(box.getName());
            if (choice == Jersey) {
                SetJerseyColor(bmp, form.getCurrentColor());
            } else {
                SetPantsColor(bmp, form.getCurrentColor());
            }
            box.setIcon(new ImageIcon(bmp));
            lbl.setText(form.getCurrentColorString());
            lbl.setBackground(form.getCurrentColor());
            //makes label readable
            if (form.getCurrentColorString().startsWith("20") || form.getCurrentColorString().startsWith("3")) {
                lbl.setForeground(Color.BLACK);
            } else {
                lbl.setForeground(Color.WHITE);
            }
            if (form.getCurrentColorString().endsWith("F") || form.getCurrentColorString().endsWith("E")) {
                lbl.setForeground(Color.WHITE);
            }

            ReplaceColorData();
        }
        form.dispose();
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(UniformEditForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(UniformEditForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(UniformEditForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(UniformEditForm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the dialog */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                UniformEditForm dialog = new UniformEditForm(new javax.swing.JFrame(), true);
                dialog.addWindowListener(new java.awt.event.WindowAdapter() {
                    @Override
                    public void windowClosing(java.awt.event.WindowEvent e) {
                        System.exit(0);
                    }
                });
                dialog.setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel10;
    private javax.swing.JLabel jLabel11;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JLabel jLabel9;
    private javax.swing.JButton mAwayJerseyButton;
    private javax.swing.JLabel mAwayJerseyLabel;
    private javax.swing.JButton mAwayPantsButton;
    private javax.swing.JLabel mAwayPantsLabel;
    private javax.swing.JLabel mAwayPictureBox;
    private javax.swing.JButton mCancelButton;
    private javax.swing.JPanel mConfChampGroupBox;
    private javax.swing.JLabel mConfChampHelmetLabel;
    private javax.swing.JLabel mConfChampPictureBox;
    private javax.swing.JLabel mConfChampUniform1Label;
    private javax.swing.JLabel mConfChampUniform2Label;
    private javax.swing.JLabel mConfChampUniform3Label;
    private javax.swing.JPanel mDivChampGroupBox;
    private javax.swing.JLabel mDivChampHelmet1Label;
    private javax.swing.JLabel mDivChampHelmet2Label;
    private javax.swing.JLabel mDivChampPictureBox;
    private javax.swing.JLabel mDivChampUniform1Label;
    private javax.swing.JLabel mDivChampUniform2Label;
    private javax.swing.JLabel mDivChampUniform3Label;
    private javax.swing.JButton mHomeJerseyButton;
    private javax.swing.JLabel mHomeJerseyLabel;
    private javax.swing.JButton mHomePantsButton;
    private javax.swing.JLabel mHomePantsLabel;
    private javax.swing.JLabel mHomePictureBox;
    private javax.swing.JButton mOKButton;
    private javax.swing.JButton mSkin1Button;
    private javax.swing.JLabel mSkin1Label;
    private javax.swing.JButton mSkin2Button;
    private javax.swing.JLabel mSkin2Label;
    private javax.swing.JComboBox mTeamsComboBox;
    private javax.swing.JPanel mUniform1GroupBox;
    private javax.swing.JPanel mUniform2GroupBox;
    private javax.swing.JButton mUniformUsageButton;
    // End of variables declaration//GEN-END:variables
}
